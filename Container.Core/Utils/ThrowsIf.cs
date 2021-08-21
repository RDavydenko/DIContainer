using System;
using Container.Core.Exceptions;
using Container.Core.Extensions;

namespace Container.Core.Utils
{
	internal static class ThrowsIf
	{
		internal static void TypeIsAbstract(string message, Type type)
		{
			if (type.IsAbstract)
			{
				throw new CannotResolveTypeException(message, type);
			}
		}

		internal static void TypeIsAbstract<T>(string message) => TypeIsAbstract(message, typeof(T));

		internal static void TypeIsPrimitive(string message, Type type)
		{
			if (type.IsPrimitive())
			{
				throw new CannotRegisterPrimitiveTypeException(message, type);
			}
		}

		internal static void TypeIsPrimitive<T>(string message) => TypeIsPrimitive(message, typeof(T));
	}
}
