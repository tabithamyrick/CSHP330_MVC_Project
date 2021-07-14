using Microsoft.EntityFrameworkCore;
using MiniStructorDB;
using MiniStructorRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserBusiness
{
    public class ClassBusiness 
    {
        public static void CreateClass(Class @class)
        {
            using (var dbContext = new minicstructorContext())
            {
                var classRepository = new Repository<Class>(dbContext);

                classRepository.Insert(@class);
            }
        }

        public static void RemoveClass(Class @class)
        {
            using (var dbContext = new minicstructorContext())
            {
                var classRepository = new Repository<Class>(dbContext);

                classRepository.Delete(@class);
            }
        }

        public static void UpdateClass(Class @class)
        {
            using (var dbContext = new minicstructorContext())
            {
                var classRepository = new Repository<Class>(dbContext);

                classRepository.Update(@class);
                
            }
        }

        public static Class FindClass(Class @class)
        {
            using (var dbContext = new minicstructorContext())
            {
                var classRepository = new Repository<Class>(dbContext);

                return classRepository.GetById(@class.ClassId);

            }
        }

        public static List<Class> GetAllClasses()
        {
            using (var dbContext = new minicstructorContext())
            {
                var classRepository = new Repository<User>(dbContext);
                var UserList = classRepository.GetAll();
                return (List<Class>)UserList;

            }
        }

    }
}
