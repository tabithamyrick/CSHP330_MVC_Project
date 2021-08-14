using MiniStructorDB;


namespace MiniStructorRepository
{
    public interface IUserRepository
    {
        UserModel LogIn(string email, string password);
        UserModel Register(User user);
    }
}


