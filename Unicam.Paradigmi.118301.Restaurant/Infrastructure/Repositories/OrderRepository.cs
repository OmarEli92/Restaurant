using Infrastructure.Context;
using Infrastructure.Extensions;
using Microsoft.Data.SqlClient;
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
        public List<Order> GetOrdersFromUser(int start, int nOfRecords,string? attribute,
                                    User? user, out int totalNumberOfOrders)
        {

            if (string.IsNullOrEmpty(attribute) || attribute.Equals("string")){
                attribute = "OrderDate";   
            }
            //var query = context.Orders.OrderByField(u => u.GetType().GetProperty(attribute));
            var query = context.Orders.AsQueryable();

            if (user != null && !(user.Role.Equals("Admin")))
            {
                query.Where(o => o.User == user);
                    
            }
            
            totalNumberOfOrders = query.Count(); // TODO modificare con orders.Capacity 
            List<Order> orders =  query.OrderByField(attribute,true).Include(o => o.OrderedDishes)
                            .Skip(start)
                          .Take(nOfRecords)
                          .ToList();
            
            return orders;
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

        /** Get a order based on its id async version**/
        public async Task<Order> GetOrderAsync(int id)
        {
            return await context.Orders
                .Where(o => o.OrderID == id)
                .FirstAsync();
        }


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

        public IEnumerable<Order> GetOrdersFromDateToDate(DateTime start, DateTime end,
                                                                      int? userId, string orderBy,
                                                                     out int totalNumberOfOrders)
        {
            var safeStart = new SqlParameter("start", start);
            var safeEnd = new SqlParameter("end", end);
            var history = new List<Order>();
            // if there is a user id to use as a filter
            if(!userId.HasValue)
            {
                history =  context.Orders.FromSqlRaw(
                    $"SELECT * FROM Orders WHERE ( OrderDate >= @start && OrderDate <= @end ) ORDER BY {orderBy}",safeStart,safeEnd)
                    .ToList();
            }
            var safeUserId = new SqlParameter("userId", userId);
            history =  context.Orders.FromSqlRaw(
                $"SELECT * FROM Orders WHERE ( OrderDate >= @start && OrderDate <= @end && UserId = @userId) ORDER BY {orderBy}", safeStart, safeEnd, userId)
                .ToList();
            totalNumberOfOrders = history.Count();
            return history;
        }
    }

}
