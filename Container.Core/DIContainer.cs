using Container.Core.Models;
using System;

namespace Container.Core
{
	public static class DIContainer
	{
		private static readonly ContainerScope globalScope;

		static DIContainer()
		{
			globalScope = new ContainerScope();
		}

		public static void Register<TConcrete>(LifetimeType lifetime = LifetimeType.Transient)
			=> Register<TConcrete>(lifetime: lifetime, factory: null);

		public static void Register<TConcrete>(Func<TConcrete> factory)
			=> Register<TConcrete>(lifetime: LifetimeType.Transient, factory: factory);

		public static void Register<TConcrete>(LifetimeType lifetime, Func<TConcrete> factory)
		{
			globalScope.Register<TConcrete>(lifetime, factory);
		}

		public static void Register<TBase, TConcrete>(LifetimeType lifetime = LifetimeType.Transient)
			where TConcrete : TBase
			=> Register<TBase, TConcrete>(lifetime: lifetime, factory: null);

		public static void Register<TBase, TConcrete>(Func<TConcrete> factory)
			where TConcrete : TBase
			=> Register<TBase, TConcrete>(lifetime: LifetimeType.Transient, factory: factory);

		public static void Register<TBase, TConcrete>(LifetimeType lifetime, Func<TConcrete> factory)
			where TConcrete : TBase
		{
			globalScope.Register<TBase, TConcrete>(lifetime, factory);
		}

		public static T Resolve<T>()
		{
			return globalScope.Resolve<T>();
		}
	}
}
