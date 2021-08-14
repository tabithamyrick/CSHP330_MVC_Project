using MiniStructorBusiness.Services;
using MiniStructorDB;
using MiniStructorRepository;
using System.Collections.Generic;
using System.Linq;

namespace MiniStructorBusiness
{
    public class UserBusiness : IUserManager
    {
        private Repository<User> userRepository;
        public UserBusiness()
        {
        }

        public static void CreateUser(User user)
        {
            var userRepository = new Repository<User>();
            user.UserPassword = EncryptPassword(user.UserPassword, user.UserEmail);
            userRepository.Insert(user);
        }

        public static void RemoveUser(User user)
        {
            var userRepository = new Repository<User>();
            userRepository.Delete(user);
        }

        public static void UpdateUser(User user)
        {

            var userRepository = new Repository<User>();
            user.UserPassword = EncryptPassword(user.UserPassword, user.UserEmail);
            userRepository.Update(user);

        }

        public User FindUser(int userId)
        {

            var userRepository = new Repository<User>();
            return userRepository.GetById(userId);


        }

        public List<User> GetAllUsers()
        {
            var userRepository = new Repository<User>();
            List<User> UserList = userRepository.GetAll().ToList();
            return UserList;

        }

        public List<Class> GetClassesForUser(int userId)
        {

            var userClassRepository = new Repository<UserClass>();
            var classRepository = new Repository<Class>();
            var userClassList = userClassRepository.GetAll().Where(x => x.UserId == userId);
            List<Class> classList = new List<Class>();
            foreach (var uc in userClassList)
            {
                var classFound = classRepository.SearchFor(x => x.ClassId == uc.ClassId).FirstOrDefault();
                classList.Add(classFound);
            }

            return classList;

        }

        public UserModel LogIn(string email, string password)
        {
            var userRepository = new UserRepository();
            var user = userRepository.LogIn(email, EncryptPassword(password, email));

            if (user == null)
            {
                return null;
            }

            return new UserModel { Id = user.Id, Name = user.Name };
        }

        public UserModel Register(User userRegistration)
        {

            var userRepository = new UserRepository();
            userRegistration.UserIsAdmin = false;
            userRegistration.UserPassword = EncryptPassword(userRegistration.UserPassword, userRegistration.UserEmail);
            var user = userRepository.Register(userRegistration);

            if (user == null)
            {
                return null;
            }

            return new UserModel { Id = user.Id, Name = user.Name };
        }

        private static string EncryptPassword(string userName, string password)
        {
            var es = new EncryptionService();
            var encryptedPassword = es.EncryptPassword(password, userName);
            return encryptedPassword;
        }

    }
}
