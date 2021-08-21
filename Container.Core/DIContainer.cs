using Container.Core.Extensions;
using System;

namespace Container.Core
{
	/// <summary>
	/// Глобальный DI-контейнер
	/// </summary>
	public static class DIContainer
	{
		private static readonly ContainerScope globalScope;

		static DIContainer()
		{
			globalScope = new ContainerScope();
		}

		/// <summary>
		/// Глобальный скоуп
		/// </summary>
		public static IContainerScope GlobalScope => globalScope;

		/// <summary>
		/// Зарегистрировать конкретный тип
		/// </summary>
		/// <typeparam name="TConcrete"></typeparam>
		public static void Register<TConcrete>()
			=> Register<TConcrete>(lifetime: Lifetime.Transient, factory: null);

		/// <summary>
		/// Зарегистрировать конкретный тип
		/// </summary>
		/// <typeparam name="TConcrete"></typeparam>
		/// <param name="lifetime">Жизненный цикл экземпляра типа</param>
		public static void Register<TConcrete>(Lifetime lifetime)
			=> Register<TConcrete>(lifetime: lifetime, factory: null);

		/// <summary>
		/// Зарегистрировать конкретный тип
		/// </summary>
		/// <typeparam name="TConcrete"></typeparam>
		/// <param name="factory">Фабрика создания экземпляра типа</param>
		public static void Register<TConcrete>(Func<TConcrete> factory)
			=> Register<TConcrete>(lifetime: Lifetime.Transient, factory: factory);

		/// <summary>
		/// Зарегистрировать конкретный тип
		/// </summary>
		/// <typeparam name="TConcrete"></typeparam>
		/// <param name="lifetime">Жизненный цикл экземпляра типа</param>
		/// <param name="factory">Фабрика создания экземпляра типа</param>
		public static void Register<TConcrete>(Lifetime lifetime, Func<TConcrete> factory)
		{
			globalScope.Register<TConcrete>(lifetime, factory);
		}

		/// <summary>
		/// Зарегистрировать базовый тип и его реализацию
		/// </summary>
		/// <typeparam name="TBase"></typeparam>
		/// <typeparam name="TConcrete"></typeparam>
		public static void Register<TBase, TConcrete>()
			where TConcrete : TBase
			=> Register<TBase, TConcrete>(lifetime: Lifetime.Transient, factory: null);

		/// <summary>
		/// Зарегистрировать базовый тип и его реализацию
		/// </summary>
		/// <typeparam name="TBase"></typeparam>
		/// <typeparam name="TConcrete"></typeparam>
		/// <param name="lifetime">Жизненный цикл экземпляра типа</param>
		public static void Register<TBase, TConcrete>(Lifetime lifetime)
			where TConcrete : TBase
			=> Register<TBase, TConcrete>(lifetime: lifetime, factory: null);

		/// <summary>
		/// Зарегистрировать базовый тип и его реализацию
		/// </summary>
		/// <typeparam name="TBase"></typeparam>
		/// <typeparam name="TConcrete"></typeparam>
		/// <param name="factory">Фабрика создания экземпляра типа</param>
		public static void Register<TBase, TConcrete>(Func<TConcrete> factory)
			where TConcrete : TBase
			=> Register<TBase, TConcrete>(lifetime: Lifetime.Transient, factory: factory);

		/// <summary>
		/// Зарегистрировать базовый тип и его реализацию
		/// </summary>
		/// <typeparam name="TBase"></typeparam>
		/// <typeparam name="TConcrete"></typeparam>
		/// <param name="lifetime">Жизненный цикл экземпляра типа</param>
		/// <param name="factory">Фабрика создания экземпляра типа</param>
		public static void Register<TBase, TConcrete>(Lifetime lifetime, Func<TConcrete> factory)
			where TConcrete : TBase
		{
			globalScope.Register<TBase, TConcrete>(lifetime, factory);
		}

		/// <summary>
		/// Получить экземпляр типа, используя зарегистированные зависимости
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T Resolve<T>()
		{
			return globalScope.Resolve<T>();
		}

		/// <summary>
		/// Получить локальный скоуп
		/// </summary>
		/// <returns></returns>
		public static IContainerScope GetLocalScope()
		{
			return globalScope.GetClearedLocalScope();
		}
	}
}
