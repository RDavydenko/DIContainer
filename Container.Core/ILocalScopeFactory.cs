using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container.Core
{
	public interface ILocalScopeFactory
	{
		IContainerScope GetLocalScope();
	}
}
