using Models.Entities;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests.Dishes
{
    public class CreateDishRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public MenuCourses Type { get; set; }

        public Dish MapToEntity()
        {
            var dish = new Dish();
            dish.Name = Name;
            dish.Description = Description;
            dish.Price = Price;
            dish.Type = Type;
            return dish;
        }
    }
}
