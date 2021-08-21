using Container.Core.Extensions;
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

		public static IContainerScope GlobalScope => globalScope;

		public static void Register<TConcrete>()
			=> Register<TConcrete>(lifetime: LifetimeType.Transient, factory: null);

		public static void Register<TConcrete>(LifetimeType lifetime)
			=> Register<TConcrete>(lifetime: lifetime, factory: null);

		public static void Register<TConcrete>(Func<TConcrete> factory)
			=> Register<TConcrete>(lifetime: LifetimeType.Transient, factory: factory);

		public static void Register<TConcrete>(LifetimeType lifetime, Func<TConcrete> factory)
		{
			globalScope.Register<TConcrete>(lifetime, factory);
		}

		public static void Register<TBase, TConcrete>()
			where TConcrete : TBase
			=> Register<TBase, TConcrete>(lifetime: LifetimeType.Transient, factory: null);

		public static void Register<TBase, TConcrete>(LifetimeType lifetime)
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

		public static IContainerScope GetLocalScope()
		{
			return globalScope.GetClearedLocalScope();
		}
	}
}
