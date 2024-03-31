using Infrastructure.Context;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DishRepository : GenericRepository<Dish>
    {
        public DishRepository(MyDBContext ctx) : base(ctx)
        {
        }
    }
}
