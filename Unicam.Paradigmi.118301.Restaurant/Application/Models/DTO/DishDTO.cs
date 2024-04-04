using Models.Entities;
using Models.Enums;


namespace Application.Models.DTO
{
    public class DishDTO
{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public MenuCourses Type { get; set; }
        public DishDTO() { }
        public DishDTO(Dish dish)
        {
            Id = dish.DishId; 
            Name = dish.Name;
            Description = dish.Description;
            Price = dish.Price;
           Type = dish.Type;
        }
}
}
