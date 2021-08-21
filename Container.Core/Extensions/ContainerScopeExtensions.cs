namespace Container.Core.Extensions
{
	/// <summary>
	/// Методы расширения для <see cref="ContainerScope"/>
	/// </summary>
	internal static class ContainerScopeExtensions
	{
		/// <summary>
		/// Получить скоуп с очищенными Scoped объектами
		/// </summary>
		/// <param name="scope">Скоуп-родитель</param>
		/// <returns>Новый независимый скоуп</returns>
		internal static ContainerScope GetClearedLocalScope(this ContainerScope scope)
		{
			var localScope = (ContainerScope)scope.Clone();
			// Transient и Singleton остаются без изменений.
			// Scoped-зависимости надо почистить:
			localScope.ClearScopedInstances();
			return localScope;
		}
	}
}
