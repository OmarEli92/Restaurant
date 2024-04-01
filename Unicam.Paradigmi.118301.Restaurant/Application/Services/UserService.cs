using Application.Abstractions.Services;
using Infrastructure.Repositories;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository userRepository;

        public UserService(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        /** Add a user in the db**/
        public async Task AddUserAsync(User user)
        {
             await userRepository.AddUserAsync(user);
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await userRepository.GetUserAsync(id);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await userRepository.GetUserByEmailAsync(email);
        }

        public List<User> GetUsers(int start, int num, out int totalNumberOfUsers)
        {
            return userRepository.GetUsers(start, num, out totalNumberOfUsers);
        }

        public async Task RemoveUserAsync(User user)
        {
            await userRepository.RemoveUserAsync(user);
        }

        public async Task UpdateUserAsync(User user)
        {
            await userRepository.UpdateUserAsync(user);
        }
    }
}
