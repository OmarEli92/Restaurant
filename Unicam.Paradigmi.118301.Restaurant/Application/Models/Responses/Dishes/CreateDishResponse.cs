using Application.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Responses.Dishes
{
    public class CreateDishResponse
    {
        public DishDTO Dish { get; set; } = null!;
    }
}
