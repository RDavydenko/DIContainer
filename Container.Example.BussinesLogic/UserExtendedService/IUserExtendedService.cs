namespace Container.Example.BussinesLogic
{
	public interface IUserExtendedService
	{
		IUserService UserService { get; }
		IUserRepository UserRepository { get; }
	}
}
