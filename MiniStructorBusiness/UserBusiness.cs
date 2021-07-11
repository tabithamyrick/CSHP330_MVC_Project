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
    }

    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class UserBusiness : IUserManager
    {
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

        public static User FindUser(User user)
        {
            using (var dbContext = new minicstructorContext())
            {
                var userRepository = new Repository<User>(dbContext);

                return userRepository.GetById(user.UserId);

            }
        }

        public static List<User> GetAllUsers()
        {
            using (var dbContext = new minicstructorContext())
            {
                var userRepository = new Repository<User>(dbContext);
                var UserList = userRepository.GetAll();
                return (List<User>)UserList;

            }
        }

        public static List<Class> GetClassesForUser(User user)
        {
            using (var dbContext = new minicstructorContext())
            {
                var userClassRepository = new Repository<UserClass>(dbContext);
                var classRepository = new Repository<Class>(dbContext);
                var classList = classRepository.GetAll();
                var userClassList = userClassRepository.GetAll();
                return (List<Class>)userClassList.Join(classList, x => x.ClassId, y => y.ClassId, (ucList, cList) => new { x = ucList, y = cList}).Where(z => z.x.UserId == user.UserId);

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
