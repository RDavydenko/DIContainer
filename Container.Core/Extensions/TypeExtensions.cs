using System;
using System.Linq;

namespace Container.Core.Extensions
{
	/// <summary>
	/// Методы расширения для <see cref="Type"/>
	/// </summary>
	internal static class TypeExtensions
	{
		private static readonly Type[] primitiveTypes = new[] { typeof(string), typeof(decimal), typeof(DateTime) };

		/// <summary>
		/// Является ли тип примитивным
		/// </summary>
		/// <param name="type">Тип</param>
		/// <returns></returns>
		internal static bool IsPrimitive(this Type type)
		{
			return type.IsPrimitive || type.IsEnum || type.IsArray || primitiveTypes.Contains(type);
		}

		/// <summary>
		/// Является ли тип примитивным
		/// </summary>
		/// <typeparam name="T">Тип</typeparam>
		/// <returns></returns>
		internal static bool IsPrimitive<T>() => IsPrimitive(typeof(T));
	}
}
