using Models.Entities;
using Models.Enums;


namespace Application.Models.DTO
{
    public class DishDTO
{
        public string Name { get; set; }
        public int Quantity{ get; set; }
        public decimal Price { get; set; }
        public MenuCourses Type { get; set; }
        
        public DishDTO() { }
        public DishDTO(Dish dish)
        {
            Name = dish.Name;
            Quantity = dish.Quantity;
            Price = dish.Price;
           Type = dish.Type;
        }
}
}
