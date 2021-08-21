using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
