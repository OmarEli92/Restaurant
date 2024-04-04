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
        public virtual DbSet<Dish>? Dishes { get; set; } = default;
        public virtual DbSet<User>? Users { get; set; }= default;
        public virtual DbSet<Order>? Orders { get; set; } = default;


    }
}

