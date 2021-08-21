using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container.Core.Extensions
{
	internal static class TypeExtensions
	{
		private static readonly Type[] primitiveTypes = new[] { typeof(string), typeof(decimal), typeof(DateTime) };

		internal static bool IsPrimitive(this Type type)
		{
			return type.IsPrimitive || type.IsEnum || type.IsArray || primitiveTypes.Contains(type);
		}

		internal static bool IsPrimitive<T>() => IsPrimitive(typeof(T));		
	}
}
