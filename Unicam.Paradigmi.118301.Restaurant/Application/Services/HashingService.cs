﻿using Abstractions.Services;
using Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UApplication.Options;

namespace Application.Services
{
    public class HashingService : IHashingService
    {
        private readonly HashingOptions options;
        public HashingService(HashingOptions options)
        {
            this.options = options;
        }

        //Generate a hashed version of the password using the unique salt for each user//
        public string HashPassword(string password, string salt)
        {
            int iter = options.Iterations;
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                Encoding.UTF8.GetBytes(salt),
                (options.Iterations),
                options.HashAlgorithm,
                options.KeySize);
            return Convert.ToHexString(hash);
        }
        
        //Check if the entered password after being hashed is the same as the password persisted
        public bool VerifyPassword(string enteredPassword,string salt, string password)
        {
            return HashPassword(enteredPassword, salt).Equals(password);  
          
        }
    }
}
