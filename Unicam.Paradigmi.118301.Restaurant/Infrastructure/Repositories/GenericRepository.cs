using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public abstract class GenericRepository<T> where T : class
    {
        protected MyDBContext context;

        public GenericRepository(MyDBContext ctx) {
            context = ctx;
        }

        public T Get(object id)
        {
            return context.Set<T>().Find(id);
        }

        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void Delete(T entity)
        {
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public void Save(T entity)
        {
            context.SaveChanges();
        }
    }
}
