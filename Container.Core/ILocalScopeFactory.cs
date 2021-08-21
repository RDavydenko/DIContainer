namespace Container.Core
{
	/// <summary>
	/// Интерфейс фабрики создания локальных скоупов
	/// </summary>
	public interface ILocalScopeFactory
	{
		/// <summary>
		/// Получить локальный скоуп
		/// </summary>
		/// <returns></returns>
		IContainerScope GetLocalScope();
	}
}
