using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container.Core.Extensions
{
	internal static class ContainerScopeExtensions
	{
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
