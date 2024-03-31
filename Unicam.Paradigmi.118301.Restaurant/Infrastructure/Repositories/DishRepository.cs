using Infrastructure.Context;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DishRepository : GenericRepository<Dish>
    {
        public DishRepository(MyDBContext ctx) : base(ctx)
        {
           
        }

        /// <summary>
        /// Get all the dishes ,ordered by the attribute provided
        /// </summary>
        /// <param name="start">Start of the result.</param>
        /// <param name="nOfRecords">Number of records to take</param>
        /// <param name="attribute">The attribute used to order the results.</param>
        /// <param name="totalNumberOfDishes">Number of records present in the db</param>
        public List<Dish> GetDishes(int start, int nOfRecords,string? attribute,out int totalNumberOfDishes)
        {
            if (string.IsNullOrEmpty(attribute))
            {
                attribute = "Name";
            }
            var query = context.Dishes
                .OrderBy(d => d.GetType().GetProperty(attribute));
            totalNumberOfDishes = query.Count();
            return query.Skip(start)
                .Take(nOfRecords)
                .ToList();
                
        }
    }
}
