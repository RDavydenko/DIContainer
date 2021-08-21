using Container.Core;
using Container.Core.Exceptions;
using Container.Example.BussinesLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

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

			var array = scope.Resolve<int[]>();
			Assert.IsNull(array);
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
			Assert.ThrowsException<CannotResolveTypeException>(scope.Resolve<IUserRepository>);
		}

		[TestMethod]
		public void TransientTest()
		{
			var scope = new ContainerScope();
			scope.Register<IUserRepository, UserRepository>(Lifetime.Transient);
			var repository1 = scope.Resolve<IUserRepository>();
			var repository2 = scope.Resolve<IUserRepository>();
			Assert.AreNotSame(repository1, repository2);
		}

		[TestMethod]
		public void SingletonTest()
		{
			var scope = new ContainerScope();
			scope.Register<IUserRepository, UserRepository>(Lifetime.Singleton);
			var repository1 = scope.Resolve<IUserRepository>();
			var repository2 = scope.Resolve<IUserRepository>();
			Assert.AreSame(repository1, repository2);
		}

		[TestMethod]
		public void EasyFactoryTest()
		{
			var scope = new ContainerScope();
			var dateTime = new DateTime(1998, 12, 31);
			scope.Register<IUserRepository, UserRepository>(factory: () => new UserRepository(0, Array.Empty<int>(), dateTime));

			var repository = scope.Resolve<IUserRepository>();
			Assert.AreEqual(dateTime, new DateTime(1998, 12, 31));
		}

		[TestMethod]
		public void HardFactoryTest()
		{
			var scope = new ContainerScope();
			scope.Register<IUserRepository, UserRepository>(Lifetime.Singleton);
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

		[TestMethod]
		public void PrimitiveTypesRegisterTest()
		{
			var scope = new ContainerScope();

			Assert.ThrowsException<CannotRegisterPrimitiveTypeException>(() => scope.Register<string>());
			Assert.ThrowsException<CannotRegisterPrimitiveTypeException>(() => scope.Register<string>(factory: () => "String"));
			Assert.ThrowsException<CannotRegisterPrimitiveTypeException>(() => scope.Register<string>(Lifetime.Singleton));
			Assert.ThrowsException<CannotRegisterPrimitiveTypeException>(() => scope.Register<string>(Lifetime.Singleton, () => "String"));
			Assert.IsNull(scope.Resolve<string>());

			Assert.ThrowsException<CannotRegisterPrimitiveTypeException>(() => scope.Register<int>());
			Assert.ThrowsException<CannotRegisterPrimitiveTypeException>(() => scope.Register<int>(factory: () => 123));
			Assert.ThrowsException<CannotRegisterPrimitiveTypeException>(() => scope.Register<int>(Lifetime.Singleton));
			Assert.ThrowsException<CannotRegisterPrimitiveTypeException>(() => scope.Register<int>(Lifetime.Singleton, () => 123));
			Assert.AreEqual(scope.Resolve<int>(), new int());

			Assert.ThrowsException<CannotRegisterPrimitiveTypeException>(() => scope.Register<decimal>());
			Assert.ThrowsException<CannotRegisterPrimitiveTypeException>(() => scope.Register<decimal>(factory: () => 123m));
			Assert.ThrowsException<CannotRegisterPrimitiveTypeException>(() => scope.Register<decimal>(Lifetime.Singleton));
			Assert.ThrowsException<CannotRegisterPrimitiveTypeException>(() => scope.Register<decimal>(Lifetime.Singleton, () => 123m));
			Assert.AreEqual(scope.Resolve<decimal>(), new decimal());

			Assert.ThrowsException<CannotRegisterPrimitiveTypeException>(() => scope.Register<DateTime>());
			Assert.ThrowsException<CannotRegisterPrimitiveTypeException>(() => scope.Register<DateTime>(factory: () => DateTime.Now));
			Assert.ThrowsException<CannotRegisterPrimitiveTypeException>(() => scope.Register<DateTime>(Lifetime.Singleton));
			Assert.ThrowsException<CannotRegisterPrimitiveTypeException>(() => scope.Register<DateTime>(Lifetime.Singleton, () => DateTime.Now));
			Assert.AreEqual(scope.Resolve<DateTime>(), new DateTime());

			Assert.ThrowsException<CannotRegisterPrimitiveTypeException>(() => scope.Register<int[]>());
			Assert.ThrowsException<CannotRegisterPrimitiveTypeException>(() => scope.Register<int[]>(factory: () => new int[0]));
			Assert.ThrowsException<CannotRegisterPrimitiveTypeException>(() => scope.Register<int[]>(Lifetime.Singleton));
			Assert.ThrowsException<CannotRegisterPrimitiveTypeException>(() => scope.Register<int[]>(Lifetime.Singleton, () => new int[0]));
			Assert.IsNull(scope.Resolve<int[]>());
		}

		[TestMethod]
		public void GlobalScopeTest()
		{
			DIContainer.Register<UserRepository>(() => new UserRepository(123, new[] { 1, 2, 3 }, new DateTime(1998, 12, 31)));
			var rep1 = DIContainer.Resolve<UserRepository>();
			var rep2 = DIContainer.GlobalScope.Resolve<UserRepository>();
			var rep3 = DIContainer.GetLocalScope().Resolve<UserRepository>();

			Assert.IsTrue(rep1.Date == rep2.Date && rep1.Date == rep3.Date && rep1.Date == new DateTime(1998, 12, 31));
			Assert.IsTrue(rep1.Number == rep2.Number && rep1.Number == rep3.Number && rep1.Number == 123);
			Assert.IsTrue(rep1.Array.SequenceEqual(rep2.Array) && rep1.Array.SequenceEqual(rep3.Array) && rep1.Array.SequenceEqual(new[] { 1, 2, 3 }));
		}

		[TestMethod]
		public void AllScopesAreUniqueObjectsTest()
		{
			var globalScope = DIContainer.GlobalScope;
			var localScope1 = DIContainer.GetLocalScope();
			var localScope2 = DIContainer.GetLocalScope();
			var localScope3 = localScope1.GetLocalScope();
			var localScope4 = globalScope.GetLocalScope();

			Assert.AreNotSame(globalScope, localScope1);
			Assert.AreNotSame(globalScope, localScope2);
			Assert.AreNotSame(globalScope, localScope3);
			Assert.AreNotSame(globalScope, localScope4);

			Assert.AreNotSame(localScope1, localScope2);
			Assert.AreNotSame(localScope1, localScope3);
			Assert.AreNotSame(localScope1, localScope4);

			Assert.AreNotSame(localScope2, localScope3);
			Assert.AreNotSame(localScope2, localScope4);

			Assert.AreNotSame(localScope3, localScope4);
		}

		[TestMethod]
		public void SingletonsInScopeTest()
		{
			var scope = new ContainerScope();
			scope.Register<DateTimeService>(lifetime: Lifetime.Singleton, factory: () => new DateTimeService(DateTime.Now));
			var service1 = scope.Resolve<DateTimeService>();

			var localScope = scope.GetLocalScope();
			var service2 = localScope.Resolve<DateTimeService>();

			Assert.AreSame(service1, service2);
			Assert.AreEqual(service1.DateTime, service2.DateTime);
		}

		[TestMethod]
		public async Task TransientsInScopeTest()
		{
			var scope = new ContainerScope();
			scope.Register<DateTimeService>(lifetime: Lifetime.Transient, factory: () => new DateTimeService(DateTime.Now));
			var service1 = scope.Resolve<DateTimeService>();

			await Task.Delay(1000);

			var localScope = scope.GetLocalScope();
			var service2 = localScope.Resolve<DateTimeService>();

			Assert.AreNotSame(service1, service2);
			Assert.AreNotEqual(service1.DateTime, service2.DateTime);
		}

		[TestMethod]
		public async Task ScopedInScopeTest()
		{
			var scope = new ContainerScope();
			scope.Register<DateTimeService>(lifetime: Lifetime.Scoped, factory: () => new DateTimeService(DateTime.Now));
			var service1 = scope.Resolve<DateTimeService>();

			await Task.Delay(1000);

			var localScope = scope.GetLocalScope();
			var service2 = localScope.Resolve<DateTimeService>();

			Assert.AreNotSame(service1, service2);
			Assert.AreNotEqual(service1.DateTime, service2.DateTime);
		}

		[TestMethod]
		public void LocalScopeDoesNotAffectOnParentScopeTest()
		{
			var scope = new ContainerScope();
			var localScope = scope.GetLocalScope();
			localScope.Register<DateTimeService>(lifetime: Lifetime.Singleton, factory: () => new DateTimeService(DateTime.Now));

			var localService = localScope.Resolve<DateTimeService>();
			var globalService = scope.Resolve<DateTimeService>();

			Assert.AreNotSame(localService, globalService);
			Assert.AreNotEqual(localService.DateTime, globalService.DateTime);
		}

		[TestMethod]
		public void GlobalScopeAffectDIContainerTest()
		{
			DIContainer.GlobalScope.Register<DateTimeService>(factory: () => new DateTimeService(new DateTime(1998, 12, 31)));

			var service = DIContainer.Resolve<DateTimeService>();

			Assert.IsNotNull(service);
			Assert.AreEqual(service.DateTime, new DateTime(1998, 12, 31));
		}
	}
}
