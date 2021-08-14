using MiniStructorDB;
using System.Collections.Generic;

namespace MiniStructorBusiness
{
    public interface IUserManager
    {
        UserModel LogIn(string email, string password);
        UserModel Register(User user);
        User FindUser(int userId);
        List<Class> GetClassesForUser(int userId);
    }
}
