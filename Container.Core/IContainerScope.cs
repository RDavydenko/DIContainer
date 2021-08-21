using System;

namespace Container.Core
{
	/// <summary>
	/// Интерфейс скоупа DI-контейнера
	/// </summary>
	public interface IContainerScope : ILocalScopeFactory, ICloneable
	{
		/// <summary>
		/// Зарегистрировать конкретный тип
		/// </summary>
		/// <typeparam name="TConcrete"></typeparam>
		void Register<TConcrete>();

		/// <summary>
		/// Зарегистрировать конкретный тип
		/// </summary>
		/// <typeparam name="TConcrete"></typeparam>
		/// <param name="lifetime">Жизненный цикл экземпляра типа</param>
		void Register<TConcrete>(Lifetime lifetime);

		/// <summary>
		/// Зарегистрировать конкретный тип
		/// </summary>
		/// <typeparam name="TConcrete"></typeparam>
		/// <param name="factory">Фабрика создания экземпляра типа</param>
		void Register<TConcrete>(Func<TConcrete> factory);

		/// <summary>
		/// Зарегистрировать конкретный тип
		/// </summary>
		/// <typeparam name="TConcrete"></typeparam>
		/// <param name="lifetime">Жизненный цикл экземпляра типа</param>
		/// <param name="factory">Фабрика создания экземпляра типа</param>
		void Register<TConcrete>(Lifetime lifetime, Func<TConcrete> factory);

		/// <summary>
		/// Зарегистрировать базовый тип и его реализацию
		/// </summary>
		/// <typeparam name="TBase"></typeparam>
		/// <typeparam name="TConcrete"></typeparam>
		void Register<TBase, TConcrete>()
			where TConcrete : TBase;

		/// <summary>
		/// Зарегистрировать базовый тип и его реализацию
		/// </summary>
		/// <typeparam name="TBase"></typeparam>
		/// <typeparam name="TConcrete"></typeparam>
		/// <param name="lifetime">Жизненный цикл экземпляра типа</param>
		void Register<TBase, TConcrete>(Lifetime lifetime)
			where TConcrete : TBase;

		/// <summary>
		/// Зарегистрировать базовый тип и его реализацию
		/// </summary>
		/// <typeparam name="TBase"></typeparam>
		/// <typeparam name="TConcrete"></typeparam>
		/// <param name="factory">Фабрика создания экземпляра типа</param>
		void Register<TBase, TConcrete>(Func<TConcrete> factory)
			where TConcrete : TBase;

		/// <summary>
		/// Зарегистрировать базовый тип и его реализацию
		/// </summary>
		/// <typeparam name="TBase"></typeparam>
		/// <typeparam name="TConcrete"></typeparam>
		/// <param name="lifetime">Жизненный цикл экземпляра типа</param>
		/// <param name="factory">Фабрика создания экземпляра типа</param>
		void Register<TBase, TConcrete>(Lifetime lifetime, Func<TConcrete> factory)
			where TConcrete : TBase;

		/// <summary>
		/// Получить экземпляр типа, используя зарегистированные зависимости
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		T Resolve<T>();
	}
}
