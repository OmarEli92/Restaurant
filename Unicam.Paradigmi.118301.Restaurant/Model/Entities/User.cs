using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public  class User
    {
        public int UserId {  get; set; }
        public String FirstName {  get; set; }
        public String LastName{ get; set; }

        public String Email { get; set; }
        public String Password { get; set; }

        public String Ruolo { get; set; } = "Cliente";
    }
}
