using Application.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;

namespace Unicam.Paradigmi._118301.Restaurant.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DishesController: ControllerBase
    {
        private readonly IDishService dishService;

        public DishesController(IDishService dishService)
        {
            this.dishService = dishService;
        }

        [HttpPost]
        [Route("all")]
        public IEnumerable<Dish> GetDishes(int start, int num , string? attribute)
        {
            int totalOfDishes = 0;
            return  dishService.GetDishes(start,num,attribute,out totalOfDishes);
        }


        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddDish(Dish dish) {
            await dishService.AddDishAsync(dish);
            return Ok("Dish added correctly");
        }
    }
}
