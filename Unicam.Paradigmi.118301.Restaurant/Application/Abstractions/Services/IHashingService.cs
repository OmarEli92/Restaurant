using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.Services
{
    public interface IHashingService
{
        string HashPassword(string password, string salt);
}
}
