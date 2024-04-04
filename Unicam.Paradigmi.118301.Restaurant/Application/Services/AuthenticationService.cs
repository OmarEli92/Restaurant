using Abstractions.Services;
using Application.Abstractions;
using Application.Abstractions.Services;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthenticationService: IAuthenticationService
{
        public string Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public string Registration(User user)
        {
            throw new NotImplementedException();
        }

        public bool VerifyCredentials(string email, string password, out User user)
        {
            user = userService.GetUserByEmail(email);
            if (user != null)
            {
                var hashedPassword = hashingService.HashPassword(password, user.Salt);

                if (hashedPassword.Equals(user.Password))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
