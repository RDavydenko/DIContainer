using Container.Core.Extensions;
using Container.Core.Models;
using Container.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Container.Tests")]

namespace Container.Core
{
	internal class ContainerScope : IContainerScope
	{
		private const string CannotResolveAbstractMessage = "Не удалось разрешить абстрактный тип или интерфейс";
		private const string CannotRegisterAbstractMessage = "Нельзя зарегистрировать абстрактный тип или интерфейс без фабрики или указания конкретного типа";
		private const string CannotRegisterPrimitiveTypeMessage = "Нельзя зарегистрировать примитивный тип данных";

		private readonly Dictionary<string, TypeInfo> dictionaryTypes;

		private ContainerScope(Dictionary<string, TypeInfo> dictionaryTypes)
		{
			if (dictionaryTypes is null)
			{
				throw new ArgumentNullException(nameof(dictionaryTypes));
			}
			this.dictionaryTypes = dictionaryTypes;
		}

		public ContainerScope()
			: this(new Dictionary<string, TypeInfo>())
		{ }

		public void Register<TConcrete>()
			=> Register<TConcrete>(lifetime: LifetimeType.Transient, factory: null);

		public void Register<TConcrete>(LifetimeType lifetime)
			=> Register<TConcrete>(lifetime: lifetime, factory: null);

		public void Register<TConcrete>(Func<TConcrete> factory)
			=> Register<TConcrete>(lifetime: LifetimeType.Transient, factory: factory);

		public void Register<TConcrete>(LifetimeType lifetime, Func<TConcrete> factory)
		{
			Register(typeof(TConcrete).FullName, typeof(TConcrete), lifetime, factory);
		}

		public void Register<TBase, TConcrete>()
			where TConcrete : TBase
			=> Register<TBase, TConcrete>(lifetime: LifetimeType.Transient, factory: null);

		public void Register<TBase, TConcrete>(LifetimeType lifetime)
			where TConcrete : TBase
			=> Register<TBase, TConcrete>(lifetime: lifetime, factory: null);

		public void Register<TBase, TConcrete>(Func<TConcrete> factory)
			where TConcrete : TBase
			=> Register<TBase, TConcrete>(lifetime: LifetimeType.Transient, factory: factory);

		public void Register<TBase, TConcrete>(LifetimeType lifetime, Func<TConcrete> factory)
			where TConcrete : TBase
		{
			Register(typeof(TBase).FullName, typeof(TConcrete), lifetime, factory);
		}

		public T Resolve<T>()
		{
			var type = typeof(T);
			var resolved = Resolve(type);
			return resolved is null
				? default(T)
				: (T)resolved;
		}

		public IContainerScope GetLocalScope()
		{
			return this.GetClearedLocalScope();
		}

		public object Clone()
		{
			var clonedDictionaryTypes = new Dictionary<string, TypeInfo>(dictionaryTypes.Count);
			foreach (var (key, value) in dictionaryTypes)
			{
				clonedDictionaryTypes.Add(key, (TypeInfo)value.Clone());
			}
			return new ContainerScope(clonedDictionaryTypes);
		}

		internal void ClearScopedInstances()
		{
			foreach (var item in dictionaryTypes)
			{
				if (item.Value.Lifetime == LifetimeType.Scoped)
				{
					item.Value.Instance = null;
				}
			}
		}

		private void Register<T>(string typeName, Type realizationType, LifetimeType lifetime, Func<T> factory)
		{
			ThrowsIf.TypeIsPrimitive(CannotRegisterPrimitiveTypeMessage, realizationType);
			if (factory is null)
			{
				ThrowsIf.TypeIsAbstract(CannotRegisterAbstractMessage, realizationType);
			}

			dictionaryTypes[typeName] = new TypeInfo
			{
				Realization = realizationType,
				Factory = factory as Func<object>,
				Lifetime = lifetime
			};
		}

		private object Resolve(Type type)
		{
			if (dictionaryTypes.TryGetValue(type.FullName, out var typeInfo))
			{
				return CreateInstance(typeInfo);
			}
			return CreateInstance(type);
		}

		private object CreateInstance(TypeInfo typeInfo)
		{
			var lifetime = typeInfo.Lifetime;
			// Singleton && has instance
			if ((lifetime == LifetimeType.Singleton || lifetime == LifetimeType.Scoped) && typeInfo.Instance != null)
			{
				return typeInfo.Instance;
			}

			var instance = typeInfo.HasFactory
				? typeInfo.GetFromFactory()
				: CreateInstance(typeInfo.Realization);

			// Singleton -> Запомнить instance
			if (typeInfo.Lifetime == LifetimeType.Singleton || lifetime == LifetimeType.Scoped)
			{
				typeInfo.Instance = instance;
			}

			return instance;
		}

		private object CreateInstance(Type type)
		{
			// Primitive type -> Null (не вызываем конструктор)
			if (type.IsPrimitive())
			{
				return null;
			}

			var ctors = type.GetConstructors();
			// Нет конструкторов -> создаем сразу конструктором по умолчанию
			if (ctors.Any() == false)
			{
				// На этом этапе должны активироваться только конкретные типы.
				// Абстрактные и интерфейсы - означает, что тип не зарегистрирован в контейнере
				ThrowsIf.TypeIsAbstract(CannotResolveAbstractMessage, type);
				return Activator.CreateInstance(type);
			}

			// Если есть параметры в конструторе, то либо значение по умолчанию, либо рекурсивно резолвим зависимости (параметры)
			var ctor = ctors.First();
			var parameters = ctor
				.GetParameters()
				.Select(x => x.HasDefaultValue
					? x.DefaultValue 
					: Resolve(x.ParameterType)
				);
			return ctor.Invoke(parameters.ToArray());
		}
	}
}
