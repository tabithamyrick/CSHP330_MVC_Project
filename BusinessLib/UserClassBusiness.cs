using Microsoft.EntityFrameworkCore;
using MiniStructorDB;
using MiniStructorRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniStructorBusiness
{
    class UserClassBusiness
    {
        public static void CreateUserClass(UserClass userClass)
        {
            using (var dbContext = new minicstructorContext())
            {
                var userClassRepository = new Repository<UserClass>(dbContext);

                userClassRepository.Insert(userClass);
            }
        }

        public static void RemoveUserClass(UserClass userClass)
        {
            using (var dbContext = new minicstructorContext())
            {
                var userClassRepository = new Repository<UserClass>(dbContext);

                userClassRepository.Delete(userClass);
            }
        }

        public static void Register(int classID, string userName)
        {
            using (var dbContext = new minicstructorContext())
            {
                var userClassRepository = new Repository<UserClass>(dbContext);
                Repository<Class> classRepo = new Repository<Class>(dbContext);
                Repository<User> userRepo = new Repository<User>(dbContext);
                UserClass userClass = new UserClass();
                userClass.Class = classRepo.GetById(classID);
                userClass.User = userRepo.SearchFor(x => x.UserEmail == userName).FirstOrDefault();
                
                userClassRepository.Insert(userClass);
            }
        }
    }
}
