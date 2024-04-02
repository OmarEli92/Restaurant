using Infrastructure.Context;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public List<Dish> GetDishes(int start, int nOfRecords, string? attribute, out int totalNumberOfDishes)
        {
            if (string.IsNullOrEmpty(attribute))
            {
                attribute = "Name";
            }
            totalNumberOfDishes = context.Dishes.Count();
            var orderByProperty = typeof(Dish).GetProperty(attribute); 
            if (orderByProperty == null)
            {
                throw new ArgumentException("Invalid attribute specified");
            }
            attribute = orderByProperty.Name; 
            IQueryable<Dish> query = context.Dishes.AsQueryable();
            return query.OrderByField(attribute, true)
                .Skip(start)
                .Take(nOfRecords)
                .ToList();
        }
    

        public async Task AddDishAsync(Dish dish)
        {
            context.Add(dish);
            await context.SaveChangesAsync();
        }

        public async Task DeleteDishByIdAsync(int dishID)
        {
            var dish = context.Dishes.FindAsync(dishID);
            context.Remove(dish);
            await context.SaveChangesAsync() ;
        }

        public async Task UpdateDishAsync(Dish dish)
        {
            context.Update(dish);
            await context.SaveChangesAsync() ;
        }

        public async Task<Dish> GetDishAsync(int id)
        {
            return await context.Dishes
                .Where(d => d.DishId == id)
                .FirstAsync();
        }

       
    }
}
