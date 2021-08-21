using System;

namespace Container.Core.Models
{
	internal class TypeInfo : ICloneable
	{
		public Type Realization { get; set; }
		public Func<object> Factory { get; set; }
		public object Instance { get; set; }
		public LifetimeType Lifetime { get; set; } = LifetimeType.Transient;
		public bool HasFactory => Factory is not null;

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
