using System;

namespace Container.Core.Models
{
	/// <summary>
	/// Информация о зарегистрированном типе
	/// </summary>
	internal class TypeInfo : ICloneable
	{
		/// <summary>
		/// Тип, реализающий зарегистрированный тип
		/// </summary>
		public Type Realization { get; set; }

		/// <summary>
		/// Фабрика создания экземпляра типа
		/// </summary>
		public Func<object> Factory { get; set; }

		/// <summary>
		/// Созданный экземпляр типа (для Scoped и Singleton)
		/// </summary>
		public object Instance { get; set; }

		/// <summary>
		/// Время жизни экземпляра типа
		/// </summary>
		public Lifetime Lifetime { get; set; } = Lifetime.Transient;

		/// <summary>
		/// Имеет ли зарегистрированный тип фабрику для создания
		/// </summary>
		public bool HasFactory => Factory is not null;

		/// <summary>
		/// Получить объект типа из фабрики
		/// </summary>
		/// <returns></returns>
		public object GetFromFactory()
		{
			if (HasFactory)
				return Factory();
			else
				throw new InvalidOperationException();
		}

		public object Clone()
		{
			return new TypeInfo()
			{
				Realization = Realization,
				Factory = Factory,
				Instance = Instance,
				Lifetime = Lifetime
			};
		}
	}
}
