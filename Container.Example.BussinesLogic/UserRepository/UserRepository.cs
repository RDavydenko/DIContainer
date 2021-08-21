using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container.Example.BussinesLogic
{
	public class UserRepository : IUserRepository
	{
		private readonly DateTime date;

		public UserRepository(DateTime? date = null)
		{
			this.date = date ?? DateTime.Now;
		}

		public DateTime DateTime => date;

		public override string ToString()
		{
			return $"Дата создания: {date:T}";
		}
	}
}
