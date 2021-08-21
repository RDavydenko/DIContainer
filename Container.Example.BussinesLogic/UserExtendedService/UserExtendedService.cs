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
