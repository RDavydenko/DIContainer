using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Container.Core;
using Container.Example.BussinesLogic;

namespace Container.Tests
{
	[TestClass]
	public class ContainerTest
	{
		[TestMethod]
		public void PrimitiveTypesActivationTest()
		{
			var scope = new ContainerScope();

			var @string = scope.Resolve<string>();
			Assert.IsNull(@string);

			var @int = scope.Resolve<int>();
			Assert.AreEqual(@int, new int());

			var @double = scope.Resolve<double>();
			Assert.AreEqual(@double, new double());

			var @decimal = scope.Resolve<decimal>();
			Assert.AreEqual(@decimal, new decimal());

			var @char = scope.Resolve<char>();
			Assert.AreEqual(@char, new char());

			var dateTime = scope.Resolve<DateTime>();
			Assert.AreEqual(dateTime, new DateTime());

			var dateTimeNullable = scope.Resolve<DateTime?>();
			Assert.AreEqual(dateTimeNullable.Value, new DateTime());

			var @object = scope.Resolve<object>();
			Assert.IsInstanceOfType(@object, typeof(object));
		}

		[TestMethod]
		public void SimpleActivationWithAndWithoutRegisterTest()
		{
			var scope = new ContainerScope();
			scope.Register<UserRepository>();
			// With Register<>()
			var concreteRepository = scope.Resolve<UserRepository>();
			Assert.IsInstanceOfType(concreteRepository, typeof(UserRepository));

			// Without Register<>()
			scope = new ContainerScope();
			concreteRepository = scope.Resolve<UserRepository>();
			Assert.IsInstanceOfType(concreteRepository, typeof(UserRepository));
		}

		[TestMethod]
		public void AdvancedActivationWithAndWithoutRegisterTest()
		{
			var scope = new ContainerScope();
			scope.Register<IUserRepository, UserRepository>();
			// Abstract type with Register<>()
			var repository = scope.Resolve<IUserRepository>();
			Assert.IsInstanceOfType(repository, typeof(IUserRepository));

			// Abstract type without Register<>() -> throws Exception
			scope = new ContainerScope();
			// TODO: �������� ��� ����������
			Assert.ThrowsException<MissingMethodException>(scope.Resolve<IUserRepository>);
		}

		[TestMethod]
		public void TransientTest()
		{
			var scope = new ContainerScope();
			scope.Register<IUserRepository, UserRepository>(LifetimeType.Transient);
			var repository1 = scope.Resolve<IUserRepository>();
			var repository2 = scope.Resolve<IUserRepository>();
			Assert.AreNotSame(repository1, repository2);
		}

		[TestMethod]
		public void SingletonTest()
		{
			var scope = new ContainerScope();
			scope.Register<IUserRepository, UserRepository>(LifetimeType.Singleton);
			var repository1 = scope.Resolve<IUserRepository>();
			var repository2 = scope.Resolve<IUserRepository>();
			Assert.AreSame(repository1, repository2);
		}

		[TestMethod]
		public void EasyFactoryTest()
		{
			var scope = new ContainerScope();
			var dateTime = new DateTime(1998, 12, 31);
			scope.Register<IUserRepository, UserRepository>(factory: () => new UserRepository(dateTime));

			var repository = scope.Resolve<IUserRepository>();
			Assert.AreEqual(dateTime, new DateTime(1998, 12, 31));
		}

		[TestMethod]
		public void HardFactoryTest()
		{
			var scope = new ContainerScope();
			scope.Register<IUserRepository, UserRepository>(LifetimeType.Singleton);
			scope.Register<IUserService>(factory: () => new UserService(scope.Resolve<IUserRepository>()));
			scope.Register<UserExtendedService>();

			var userService = scope.Resolve<IUserService>();
			var userExtendedService = scope.Resolve<UserExtendedService>();

			Assert.IsNotNull(userService);
			Assert.IsInstanceOfType(userService, typeof(UserService));

			Assert.IsNotNull(userService.UserRepository);
			Assert.IsInstanceOfType(userService.UserRepository, typeof(UserRepository));

			Assert.IsNotNull(userExtendedService);
			Assert.IsInstanceOfType(userExtendedService, typeof(UserExtendedService));

			Assert.IsNotNull(userExtendedService.UserRepository);
			Assert.IsInstanceOfType(userExtendedService.UserRepository, typeof(UserRepository));

			Assert.IsNotNull(userExtendedService.UserService);
			Assert.IsInstanceOfType(userExtendedService.UserService, typeof(UserService));

			Assert.IsNotNull(userExtendedService.UserService.UserRepository);
			Assert.IsInstanceOfType(userExtendedService.UserService.UserRepository, typeof(UserRepository));
		}
	}
}
