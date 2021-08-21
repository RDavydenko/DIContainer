using Container.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container.Core.Utils
{
	internal static class ThrowsIf
	{
		internal static void TypeIsAbstract(Type type)
		{
			if (type.IsAbstract)
			{
				// TODO: свой Exception и передача message
				throw new Exception();
			}
		}

		internal static void TypeIsAbstract<T>() => TypeIsAbstract(typeof(T));

		internal static void TypeIsPrimitive(Type type)
		{
			if (type.IsPrimitive())
			{
				// TODO: свой Exception и передача message
				throw new Exception();
			}
		}

		internal static void TypeIsPrimitive<T>() => TypeIsPrimitive(typeof(T));
	}
}
