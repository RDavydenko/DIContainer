using Container.Core;
using Container.Example.BussinesLogic;
using System;

namespace Container.Example
{
	class Program
	{
		static void Main()
		{
			DIContainer.Register<IUserRepository, UserRepository>(Lifetime.Singleton);
			DIContainer.Register<IUserService, UserService>();
			DIContainer.Register<IUserExtendedService, UserExtendedService>();

			var concreteRepository = DIContainer.Resolve<UserRepository>();
			var service = DIContainer.Resolve<IUserService>();
			var extendedService = DIContainer.Resolve<IUserExtendedService>();
			var extendedService2 = DIContainer.Resolve<IUserExtendedService>();

			Console.ReadKey();
		}
	}
}
