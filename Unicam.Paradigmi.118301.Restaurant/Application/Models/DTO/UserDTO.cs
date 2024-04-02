using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTO
{
    public class UserDTO
{
        
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }  
        public string Role { get; set; }

        public UserDTO() { }

        public UserDTO(User user)
        {

        }
}
}
