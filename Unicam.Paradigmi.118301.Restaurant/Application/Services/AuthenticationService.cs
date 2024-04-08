using Abstractions.Services;
using Application.Abstractions;
using Application.Abstractions.Services;
using Application.Models.Requests;
using Application.Models.Requests.Users;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthenticationService: IAuthenticationService
{
        private readonly IUserService userService;
        private readonly IHashingService hashingService;
        private readonly ITokenService tokenService;
        
        public AuthenticationService(IUserService userService, IHashingService hashingService,
                                     ITokenService tokenService)
        {
            this.userService = userService;
            this.hashingService = hashingService;
            this.tokenService = tokenService;
        }

        public string Login(string email, string password)
        {
            User user = null!;
            if(VerifyCredentials(email,password,out user))
            {
                var request = new CreateTokenRequest();
                request.User = user;
                return tokenService.CreateToken(request);
            }
            throw new Exception("Email and password not valid!");
        }

        public async Task<string> Registration(User user)
        {
            if (await userService.GetUserByEmailAsync(user.Email) == null)
            {
                throw new Exception("The email is already used by another customer");
            }
            var request = new CreateTokenRequest();
            request.User = user;
            return tokenService.CreateToken(request);

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
