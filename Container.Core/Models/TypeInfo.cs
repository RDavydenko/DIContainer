using System;

namespace Container.Core.Models
{
	internal class TypeInfo
	{
		public Type Realization { get; set; }
		public Func<object> Factory { get; set; }
		public object Instance { get; set; }
		public LifetimeType Lifetime { get; set; } = LifetimeType.Transient;

		public bool HasFactory => Factory != null;
		public object GetFromFactory()
		{
			if (Factory != null)
				return Factory();
			else
				throw new Exception();
		}
	}
}
