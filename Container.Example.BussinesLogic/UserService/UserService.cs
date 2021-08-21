using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container.Example.BussinesLogic
{
	public class UserService : IUserService
	{
		public UserService(IUserRepository repository)
		{
			UserRepository = repository;
		}

		public IUserRepository UserRepository { get; }
	}
}
