using MiniStructorDB;
using System.Linq;


namespace MiniStructorRepository
{
    public class UserRepository : IUserRepository
    {

        public UserModel LogIn(string email, string password)
        {
            var userRepository = new Repository<User>();
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
            var dbContext = new minicstructorContext();
            var userRepository = new Repository<User>();

            var user = userRepository.SearchFor(x => x.UserEmail == userRegistration.UserEmail).FirstOrDefault();
            //add user if none exists
            if (user == null)
            {
                user = userRegistration;
                userRepository.Insert(userRegistration);
                dbContext.SaveChanges();
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


