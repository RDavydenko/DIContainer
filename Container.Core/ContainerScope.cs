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
	internal class ContainerScope
	{
		private readonly Dictionary<string, TypeInfo> dictionaryTypes;

		public ContainerScope()
		{
			dictionaryTypes = new Dictionary<string, TypeInfo>();
		}

		public void Register<TConcrete>(LifetimeType lifetime = LifetimeType.Transient, Func<TConcrete> factory = null)
		{
			Register(typeof(TConcrete).FullName, typeof(TConcrete), lifetime, factory);
		}

		public void Register<TBase, TConcrete>(LifetimeType lifetime = LifetimeType.Transient, Func<TConcrete> factory = null)
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

		private void Register<T>(string typeName, Type realizationType, LifetimeType lifetime, Func<T> factory)
		{
			ThrowsIf.TypeIsPrimitive(realizationType);
			if (factory is null)
			{
				ThrowsIf.TypeIsAbstract(realizationType);
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
			if (lifetime == LifetimeType.Singleton && typeInfo.Instance != null)
			{
				return typeInfo.Instance;
			}

			var instance = typeInfo.HasFactory
				? typeInfo.GetFromFactory()
				: CreateInstance(typeInfo.Realization);

			// Singleton -> Запомнить instance
			if (typeInfo.Lifetime == LifetimeType.Singleton)
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
				if (type.IsAbstract)
				{
					// TODO: throw NotRegisterException
				}
				return Activator.CreateInstance(type);
			}

			// Если есть параметры в конструторе, то либо значение по умолчанию, либо рекурсивно резолвим зависимости (параметры)
			var ctor = ctors.First();
			var parameters = ctor.GetParameters().Select(x => x.HasDefaultValue ? x.DefaultValue : Resolve(x.ParameterType));
			return ctor.Invoke(parameters.ToArray());
		}
	}
}
