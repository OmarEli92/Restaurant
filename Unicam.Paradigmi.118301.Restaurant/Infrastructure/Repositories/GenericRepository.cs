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

        public IEnumerable<T> GetAllWithPagination<T>(int start, int num,Type type,
                                                    string attribute, out int totalNumber)
        {
            var entityType = typeof(T);
            var property = entityType.GetProperty(attribute);
            if (property == null)
            {
                throw new ArgumentException($"The property '{attribute}' doesn't exist in th entity '{entityType.Name}'.");
            }
            var query = context.GetType().GetProperty(entityType.Name + "s")
                .GetValue(context) as List<T>;
            if (query == null)
            {
                throw new InvalidOperationException($"Entity not found in the context!");
            }
            var orderedQuery = query.OrderBy(item => property.GetValue(item)).ToList();
            totalNumber = orderedQuery.Count;
            return orderedQuery
                .Skip(start)
                .Take(num)
                .ToList();
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
