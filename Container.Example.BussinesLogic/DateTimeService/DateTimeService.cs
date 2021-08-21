using System;

namespace Container.Example.BussinesLogic
{
	public class DateTimeService
	{
		public DateTimeService(DateTime dateTime)
		{
			DateTime = dateTime;
		}

		public DateTime DateTime { get; }
	}
}
