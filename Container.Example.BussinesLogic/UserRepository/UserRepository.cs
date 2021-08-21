using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container.Example.BussinesLogic
{
	public class UserRepository : IUserRepository
	{
		public UserRepository(int number, int[] array, DateTime? date = null)
		{
			Number = number;
			Array = array;
			Date = date ?? DateTime.Now;
		}

		public int Number { get; }
		public int[] Array { get; }
		public DateTime Date { get; }

		public override string ToString()
		{
			return $"Дата создания: {Date:T}";
		}
	}
}
