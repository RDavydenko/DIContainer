using Container.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
	}
}
