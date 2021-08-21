using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container.Example.BussinesLogic
{
	public class UserExtendedService : IUserExtendedService
	{
		public UserExtendedService(IUserService userService, IUserRepository userRepository)
		{
			UserService = userService;
			UserRepository = userRepository;
		}

		public IUserService UserService { get; }
		public IUserRepository UserRepository { get; }
	}
}
