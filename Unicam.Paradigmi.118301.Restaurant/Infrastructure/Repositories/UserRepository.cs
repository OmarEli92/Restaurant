using Infrastructure.Configuration;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(MyDBContext ctx) : base(ctx)
        {
        }

        /** Get all users ordered by email**/
        public List<User> GetUsers(int start, int num, out int totalNumberOfUsers)
        {
            var query = context.Users
                .OrderBy(u => u.Email)
                .ToList();
            totalNumberOfUsers = query.Count();
            return query;
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

        public async Task<User> AddUserAsync(User user)
        {
            await context.AddAsync(user);
            await context.SaveChangesAsync();
            return user;
        }
     
        public async Task RemoveUserAsync(User user)
        {
            var userEntry = context.Entry(user);
            Delete(user);
            await context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            Update(user);
            await context.SaveChangesAsync();
        }
    }
}
