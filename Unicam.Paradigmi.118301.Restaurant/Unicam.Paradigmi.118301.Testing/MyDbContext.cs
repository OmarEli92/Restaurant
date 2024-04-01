using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicam.Paradigmi._118301.Testing
{
    public class MyDbContext:DbContext
    {
        //entities
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("data source=localhost;Initial catalog=paradigmi;User Id=1234;Password=1234;TrustServerCertificate=True");
        }
    }
}
