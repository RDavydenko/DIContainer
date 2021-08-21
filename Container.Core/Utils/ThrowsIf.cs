using Container.Core.Exceptions;
using Container.Core.Extensions;
using System;

namespace Container.Core.Utils
{
	/// <summary>
	/// Вспомогательный класс для выбрасывания исключений
	/// </summary>
	internal static class ThrowsIf
	{
		/// <summary>
		/// Выбрасывает исключение, если тип является абстрактным или интерфейсом
		/// </summary>
		/// <param name="message">Сообщение исключения</param>
		/// <param name="type">Проверяемый тип</param>
		internal static void TypeIsAbstract(string message, Type type)
		{
			// TODO: Может еще подумать насчет закрытого конструктора
			if (type.IsAbstract)
			{
				throw new CannotResolveTypeException(message, type);
			}
		}

		/// <summary>
		/// Выбрасывает исключение, если тип является абстрактным или интерфейсом
		/// </summary>
		/// <typeparam name="T">Проверяемый тип</typeparam>
		/// <param name="message">Сообщение исключения</param>
		internal static void TypeIsAbstract<T>(string message) => TypeIsAbstract(message, typeof(T));

		/// <summary>
		/// Выбрасывает исключение, если тип является примитивным
		/// </summary>
		/// <param name="message">Сообщение исключения</param>
		/// <param name="type">Проверяемый тип</param>
		internal static void TypeIsPrimitive(string message, Type type)
		{
			if (type.IsPrimitive())
			{
				throw new CannotRegisterPrimitiveTypeException(message, type);
			}
		}
		/// <summary>
		/// Выбрасывает исключение, если тип является примитивным
		/// </summary>
		/// <typeparam name="T">Проверяемый тип</typeparam>
		/// <param name="message">Сообщение исключения</param>
		internal static void TypeIsPrimitive<T>(string message) => TypeIsPrimitive(message, typeof(T));
	}
}
