using Application.Models.Requests.Users;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface IAuthenticationService
{
        bool VerifyCredentials(string email, string password, out User user);
        string Login(string email, string password);

        Task<string> Registration(User user);
        
}
}
