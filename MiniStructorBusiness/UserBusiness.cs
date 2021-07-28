using Microsoft.EntityFrameworkCore;
using MiniStructorDB;
using MiniStructorRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MiniStructorBusiness
{
    public interface IUserManager
    {
        UserModel LogIn(string email, string password);
        UserModel Register(User user);
        User FindUser(int userId);
        List<Class> GetClassesForUser(int userId);
    }

    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class UserBusiness : IUserManager
    {
        private UserRepository userRepository;
        public UserBusiness()
        {
        }

        public static void CreateUser(User user)
        {

            //To add --Password Hash
            //check for exsisting
            var userRepository = new Repository<User>();
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
                var classFound = classRepository.SearchFor(x => x.ClassId == uc.ClassId).FirstOrDefault() ;
                classList.Add(classFound);
            }
         
            return classList;

        }

        public UserModel LogIn(string email, string password)
        {
            using (var dbContext = new minicstructorContext())
            {
                var userRepository = new UserRepository();

                var user = userRepository.LogIn(email, password);

                if (user == null)
                {
                    return null;
                }

                return new UserModel { Id = user.Id, Name = user.Name };
            }
        }

        public UserModel Register(User userRegistration)
        {

            var userRepository = new UserRepository();
            userRegistration.UserIsAdmin = false;
            var user = userRepository.Register(userRegistration);

            if (user == null)
            {
                return null;
            }

            return new UserModel { Id = user.Id, Name = user.Name };
        }
               
    }
}
