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
