using Microsoft.EntityFrameworkCore;
using MiniStructorDB;
using MiniStructorRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniStructorBusiness
{
    public class UserClassBusiness
    {
        public void CreateUserClass(UserClass userClass)
        {
                var userClassRepository = new Repository<UserClass>();
                userClassRepository.Insert(userClass);
        }

        public void RemoveUserClass(UserClass userClass)
        {
                var userClassRepository = new Repository<UserClass>();
        }

        public void Register(int classID, string userName)
        {
                var userClassRepository = new Repository<UserClass>();
                Repository<User> userRepo = new Repository<User>();
                UserClass userClass = new UserClass();
                userClass.ClassId = classID;
                userClass.UserId = userRepo.SearchFor(x => x.UserEmail == userName).FirstOrDefault().UserId;
                userClassRepository.Insert(userClass);
        }
    }
}
