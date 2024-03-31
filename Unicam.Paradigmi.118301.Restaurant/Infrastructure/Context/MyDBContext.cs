using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class MyDBContext: DbContext
    {
        public MyDBContext(): base()
           { 
        }

        public MyDBContext(DbContextOptions<MyDBContext> configuration): base(configuration)
        {

        }
        // SET THE ENTITIES PRESENT IN THE MODEL
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}

