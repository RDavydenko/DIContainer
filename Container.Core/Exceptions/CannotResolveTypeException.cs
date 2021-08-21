using System;

namespace Container.Core.Exceptions
{
	/// <summary>
	/// Исключение, создаваемое, когда невозможно получить экземпляр типа
	/// </summary>
	public class CannotResolveTypeException : Exception
	{
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="message">Сообщение об ошибке</param>
		/// <param name="type">Тип, экземпляр которого не удалось получить</param>
		public CannotResolveTypeException(string message, Type type)
			: base(message)
		{
			Type = type;
		}

		/// <summary>
		/// Тип, экземпляр которого не удалось получить
		/// </summary>
		public Type Type { get; }
	}
}
