using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    /** This class represents a dish that can be ordered by a customer**/
    public class Dish
    {
        public int DishId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public MenuCourses Type { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        
    }
}
