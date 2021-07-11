using Microsoft.EntityFrameworkCore;
using MiniStructorDB;
using MiniStructorRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserBusiness
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
    }
}
