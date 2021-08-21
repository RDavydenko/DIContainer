namespace Container.Core
{
	/// <summary>
	/// Время жизни объекта
	/// </summary>
	public enum Lifetime
	{
		/// <summary>
		/// Создается каждый раз заново
		/// </summary>
		Transient = 0,

		/// <summary>
		/// Создается один раз для одного скоупа
		/// </summary>
		Scoped = 1,

		/// <summary>
		/// Создается только один раз
		/// </summary>
		Singleton = 2
	}
}
