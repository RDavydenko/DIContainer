using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container.Example.BussinesLogic
{
	public interface IUserService
	{
		IUserRepository UserRepository { get; }
	}
}
