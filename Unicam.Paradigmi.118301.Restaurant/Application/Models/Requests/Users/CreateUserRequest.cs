using Application.Models.DTO;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests.Users
{
    public class CreateUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public User MaptoEntity()
        {
            var user = new User();
            user.FirstName = FirstName;
            user.LastName = LastName;
            user.Email = Email;
            user.Password = Password;
            return user;
        }
    }
}
