using Microsoft.EntityFrameworkCore;
using MiniStructorDB;
using MiniStructorRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniStructorBusiness
{
    public class ClassBusiness 
    {
        public void CreateClass(Class _class)
        {
                var classRepository = new Repository<Class>();
                classRepository.Insert(_class);
        }

        public void RemoveClass(Class _class)
        {
                var classRepository = new Repository<Class>();
                classRepository.Delete(_class);
        }

        public void UpdateClass(Class _class)
        {

                var classRepository = new Repository<Class>();
                classRepository.Update(_class);
        }

        public Class FindClass(Class _class)
        {
                var classRepository = new Repository<Class>();
                return classRepository.GetById(_class.ClassId);
        }

        public List<Class> GetAllClasses()
        {
                var classRepository = new Repository<Class>();
                var classes = classRepository.GetAll().ToList<Class>();
                return classes;
        }

    }
}
