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
        /** Get all users ordered by email**/
        public List<Order> GetOrders(int start, int num,string attribute, out int totalNumberOfOrders)
        {
            var query = context.Orders
                .OrderBy(u => u.GetType().GetProperty(attribute));

            totalNumberOfOrders = query.Count();
            return query.Skip(start)
                .Take(num)
                .ToList();
                
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
