using Infrastructure.Repositories;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Services
{
    public interface IUserService
    {
        List<User> GetUsers(int start, int num, out int totalNumberOfUsers);
        Task<User> GetUserAsync(int id);
        Task<User> GetUserByEmailAsync(string email);

        Task AddUserAsync(User user);

        Task RemoveUserAsync(User user);

        Task UpdateUserAsync(User user);






    }
}
