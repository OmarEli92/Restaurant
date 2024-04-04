using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public List<Order> GetOrders(int start, int nOfRecords,string? attribute,
                                    User? user, out int totalNumberOfOrders)
        {

            if (string.IsNullOrEmpty(attribute)){
                attribute = "OrderDate";   
            }
            var query = context.Orders.OrderBy(u => u.GetType().GetProperty(attribute));
            if (user != null)
            {
                query.Where(o => o.OrderedByUser == user);
            }
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
        public  int AddOrder(Order order, out decimal totalCheck)
        {
            totalCheck = order.TotalCheck;
            context.Orders.Add(order);
            context.SaveChanges();
            return  order.OrderID;
        }

        /** Get a order based on its id**/
        public async Task<Order> GetOrderAsync(int id)
        {
            return await context.Orders
                .Where(o => o.OrderID == id)
                .FirstAsync();
        }

        //TODO da testare se va bene generico altrimenti lasciare questo metodo 
        /** Get a prder based on its email**/
        /*
        public List<Order> GetOrdersFromUser(User user,int start, int nOfRecords, out int totalNumberOfOrders )
        {
            var query = context.Orders
                        .Where(o => o.OrderedByUser == user);
            totalNumberOfOrders = query.Count();
            return query.Skip(start)
                .Take(nOfRecords)
                .ToList();
        }
        */

        public async Task RemoveOrderAsync(Order order)
        {
            Delete(order);
            await context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            Update(order);
            await context.SaveChangesAsync();
            
        }
    }

}
