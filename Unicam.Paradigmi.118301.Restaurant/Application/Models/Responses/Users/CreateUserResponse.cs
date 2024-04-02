using Application.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Responses.Users
{
    public class CreateUserResponse
    {
        public UserDTO User { get; set; } = null!;
    }
}
