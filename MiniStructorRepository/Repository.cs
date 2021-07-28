using Microsoft.EntityFrameworkCore;
using MiniStructorDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MiniStructorRepository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbSet<T> DbSet;
        DbContext dataContext = new minicstructorContext();

        public Repository()
        {
            DbSet = dataContext.Set<T>();
        }

        public void Insert(T entity)
        {
                DbSet.Add(entity);
                dataContext.SaveChanges();
        }

        public void Update(T entity)
        {
            DbSet.Update(entity);
            dataContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
            dataContext.SaveChanges();
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public T GetById(int id)
        {
            return DbSet.Find(id);
        }

    }
}
