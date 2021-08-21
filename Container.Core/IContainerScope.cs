using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container.Core
{
	public interface IContainerScope : ILocalScopeFactory, ICloneable
	{
		void Register<TConcrete>();
		void Register<TConcrete>(LifetimeType lifetime);
		void Register<TConcrete>(Func<TConcrete> factory);
		void Register<TConcrete>(LifetimeType lifetime, Func<TConcrete> factory);

		void Register<TBase, TConcrete>()
			where TConcrete : TBase;
		void Register<TBase, TConcrete>(LifetimeType lifetime)
			where TConcrete : TBase;
		void Register<TBase, TConcrete>(Func<TConcrete> factory)
			where TConcrete : TBase;
		void Register<TBase, TConcrete>(LifetimeType lifetime, Func<TConcrete> factory)
			where TConcrete : TBase;

		T Resolve<T>();
	}
}
