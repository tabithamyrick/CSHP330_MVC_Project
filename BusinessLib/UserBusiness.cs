using Microsoft.EntityFrameworkCore;
using MiniStructorDB;
using MiniStructorRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserBusiness
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
        public UserBusiness()
        {
        }

        public static void CreateUser(User user)
        {

            //To add --Password Hash
            //check for exsisting
            using (var dbContext = new minicstructorContext())
            {
                var userRepository = new Repository<User>(dbContext);

                userRepository.Insert(user);
            }
        }

        public static void RemoveUser(User user)
        {
            using (var dbContext = new minicstructorContext())
            {
                var userRepository = new Repository<User>(dbContext);

                userRepository.Delete(user);
            }
        }

        public static void UpdateUser(User user)
        {
            using (var dbContext = new minicstructorContext())
            {
                var userRepository = new Repository<User>(dbContext);

                userRepository.Update(user);

            }
        }

        public User FindUser(int userId)
        {
            using (var dbContext = new minicstructorContext())
            {
                var userRepository = new Repository<User>(dbContext);

                return userRepository.GetById(userId);

            }
        }

        public List<User> GetAllUsers()
        {
            using (var dbContext = new minicstructorContext())
            {
                var userRepository = new Repository<User>(dbContext);
                List<User> UserList = userRepository.GetAll().ToList();
                return UserList;

            }
        }

        public List<Class> GetClassesForUser(int userId)
        {
            using (var dbContext = new minicstructorContext())
            {
                var userClassRepository = new Repository<UserClass>(dbContext);
                var classRepository = new Repository<Class>(dbContext);
                var userClassList = userClassRepository.GetAll().Where(x => x.UserId == userId).ToList();
                //var classList = classRepository.GetAll().SelectMany();
                //var returnList = classList.Join(userClassList, x => x.ClassId, y => y.ClassId, (cList, ucList) => new { x = cList, y = ucList })
                //    .Select(x => new Class
                //    {
                //        ClassId = x.x.ClassId,
                //        ClassName = x.x.ClassName,
                //        ClassDescription = x.x.ClassDescription,
                //        ClassPrice = x.x.ClassPrice,
                //        UserClasses = x.x.UserClasses
                //    }).ToList();
                return null;

            }
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

            using (var dbContext = new minicstructorContext())
            {
                var userRepository = new UserRepository();
                //var nextID = dbContext.Users.Count() + 1;
                //userRegistration.UserId = nextID;
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
}
