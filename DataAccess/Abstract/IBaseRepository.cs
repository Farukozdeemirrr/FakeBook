using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IBaseRepository <T> where T : class
    {
        IQueryable<T> GetAll(FakeBookDbContext context);
        T Add(FakeBookDbContext context, T entity);
        T Update(FakeBookDbContext context, T entity);
        void Delete(FakeBookDbContext context, long id);
        T GetById(FakeBookDbContext context, long id);
    }
}
