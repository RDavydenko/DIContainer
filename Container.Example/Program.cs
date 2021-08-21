using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Container.Core;

namespace Container.Example
{
	class Program
	{
		static void Main()
		{
			DIContainer.Register<IUserRepository, UserRepository>(LifetimeType.Singleton);
			DIContainer.Register<IUserService, UserService>();
			DIContainer.Register<IUserExtendedService, UserExtendedService>();

			var concreteRepository = DIContainer.Resolve<UserRepository>();
			var service = DIContainer.Resolve<IUserService>();
			var extendedService = DIContainer.Resolve<IUserExtendedService>();
			Thread.Sleep(5000);
			var extendedService2 = DIContainer.Resolve<IUserExtendedService>();

			Console.ReadKey();
		}
	}


	interface IUserRepository
	{

	}

	public class UserRepository : IUserRepository
	{
		private readonly DateTime date;

		public UserRepository(DateTime? date = null)
		{
			this.date = date ?? DateTime.Now;
		}

		public override string ToString()
		{
			return $"Дата создания: {date:T}";
		}
	}

	interface IUserService
	{

	}

	class UserService : IUserService
	{
		private readonly IUserRepository repository;

		public UserService(IUserRepository repository)
		{
			this.repository = repository;
		}
	}

	interface IUserExtendedService
	{

	}

	class UserExtendedService : IUserExtendedService
	{
		private readonly IUserService userService;
		private readonly IUserRepository userRepository;

		public UserExtendedService(IUserService userService, IUserRepository userRepository)
		{
			this.userService = userService;
			this.userRepository = userRepository;
		}
	}
}
