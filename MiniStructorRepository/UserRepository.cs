using System;
using MiniStructorDB;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MiniStructorRepository
{

    public interface IUserRepository
    {
        UserModel LogIn(string email, string password);
        UserModel Register(User user);
    }

    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UserRepository : IUserRepository
    {

        public UserModel LogIn(string email, string password)
        {

            using var dbContext = new minicstructorContext();
            var userRepository = new Repository<User>(dbContext);

            var user = userRepository.SearchFor(x => x.UserEmail == email && x.UserPassword == password).FirstOrDefault();
            if (user == null)
            {
                return null;
            }

            return new UserModel { Id = user.UserId, Name = user.UserEmail };

        }


        public UserModel Register(User userRegistration)
        {
            //check for existing user
            using var dbContext = new minicstructorContext();
            var userRepository = new Repository<User>(dbContext);

            var user = userRepository.SearchFor(x => x.UserEmail == userRegistration.UserEmail).FirstOrDefault();
            //add user if none exists
            if (user == null)
            {
                userRepository.Insert(userRegistration);
                //return user
                return new UserModel { Id = user.UserId, Name = user.UserEmail };
            }
            else
            {
                //return null if user exists 
                return null;
            }
        }
    }
}

