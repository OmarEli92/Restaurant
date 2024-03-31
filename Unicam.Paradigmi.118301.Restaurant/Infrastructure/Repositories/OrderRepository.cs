using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order>
    {
        public OrderRepository(MyDBContext ctx) : base(ctx)
        {
        }

        /// <summary>
        /// Get all the orders ,ordered by the attribute provided
        /// </summary>
        /// <param name="attribute">The attribute used to order the results.</param>
        /// <param name="start">Start of the result.</param>
        /// <param name="nOfRecords">Number of records to take</param>
        /// <param name="totalNumberOfOrders">Number of records present in the db</param>
        public List<Order> GetOrders(int start, int nOfRecords,string? attribute, out int totalNumberOfOrders)
        {
            if (string.IsNullOrEmpty(attribute)){
                attribute = "OrderDate";
;            }
           var query = context.Orders.OrderBy(u => u.GetType().GetProperty(attribute));
           totalNumberOfOrders = query.Count();
           return query.Skip(start)
                    .Take(nOfRecords)
                    .ToList();
           
        
        }


        /// <summary>
        /// Add a new order and calculate the total check
        /// </summary>
        /// <param name="order">The order to add.</param>
        /// <param name="totalCheck">the total check to pay.</param>
        public int AddOrder(Order order, out decimal totalCheck)
        {
            totalCheck = order.OrderedDishes.Sum(d => d.Price);
            context.Orders.Add(order);
            context.SaveChanges();
            return order.OrderNumber;
        }

        /** Get a User based on its id**/
        public async Task<User> GetUserAsync(int id)
        {
            return await context.Users
                .Where(u => u.UserId == id)
                .FirstAsync();

        }


        /** Get a User based on its email**/
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await context.Users
                .Where(u => u.Email == email)
                .FirstAsync();
        }


    }
}
