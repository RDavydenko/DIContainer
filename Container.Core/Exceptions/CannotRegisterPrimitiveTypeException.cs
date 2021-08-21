using System;

namespace Container.Core.Exceptions
{
	/// <summary>
	/// Исключение, создаваемое, когда невозможно зарегистрировать тип, т.к. он является примитивным
	/// </summary>
	public class CannotRegisterPrimitiveTypeException : Exception
	{
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="message">Сообщение об ошибке</param>
		/// <param name="type">Тип, который не удалось зарегистрировать</param>
		public CannotRegisterPrimitiveTypeException(string message, Type type)
			: base(message)
		{
			Type = type;
		}

		/// <summary>
		/// Тип, который не удалось зарегистрировать
		/// </summary>
		public Type Type { get; }
	}
}
