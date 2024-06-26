﻿using Abstractions.Services;
using Application.Abstractions.Services;
using Infrastructure.Repositories;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository userRepository;
        private readonly IHashingService hashingService;

        public UserService(UserRepository userRepository, IHashingService hashingService)
        {
            this.userRepository = userRepository;
            this.hashingService = hashingService;
        }

        /** Add a user in the db, but first it generates a hashed password**/
        public async Task AddUserAsync(User user)
        {
            var hashedPassword = hashingService.HashPassword(user.Password, user.Salt);
            user.Password = hashedPassword;
            await userRepository.AddUserAsync(user);
        }

        
        public async Task<User> GetUserAsync(int id)
        {
            return await userRepository.GetUserAsync(id);
        }

        public User GetUserByEmail(string email)
        {
            return userRepository.GetUserByEmail(email);
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await userRepository.GetUserByEmailAsync(email);
        }

        public List<User> GetUsers(int start, int num, out int totalNumberOfUsers)
        {
            return userRepository.GetUsers(start, num, out totalNumberOfUsers);
        }

        public async Task RemoveUserAsync(int id)
        {
            await userRepository.RemoveUserAsync(id);
        }

        public async Task UpdateUserAsync(User user)
        {
            await userRepository.UpdateUserAsync(user);
        }
    }
}
