using Business;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrate
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public T Add(FakeBookDbContext context, T entity)
        {
            context.Set<T>().Add(entity);
            return entity;
        }

        public void Delete(FakeBookDbContext context, long id)
        {
            var deleteEntity = GetById(context, id);
            context.Set<T>().Remove(deleteEntity);  
        }

        public IQueryable<T> GetAll(FakeBookDbContext context)
        {
            return context.Set<T>();
        }

        public T GetById(FakeBookDbContext context, long id)
        {
            return context.Set<T>().Find(id);
        }

        public T Update(FakeBookDbContext context, T entity)
        {
            context.Set<T>().Update(entity);
            return entity;
        }
    }
}
