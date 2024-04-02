using Application.Abstractions.Services;
using Application.Factories;
using Application.Models.Requests.Dishes;
using Application.Models.Responses.Dishes;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using Models.Responses;

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
        public async Task<IActionResult> AddDish(CreateDishRequest request) {
            var dish = request.MapToEntity();
            await dishService.AddDishAsync(dish);
            var response = new CreateDishResponse();
            response.Dish = new Application.Models.DTO.DishDTO(dish);
            return Ok(ResponseFactory.WithSuccess(response));
        }

        [HttpPost]
        [Route("remove")]
        public async Task<IActionResult> RemovoveDishByIdFromMenu(int dishID)
        {
            await dishService.RemoveDishByIdAsync(dishID);
            return Ok("Dish removed!");
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateDish(Dish dish)
        {
            await dishService.UpdateDishAsync(dish);
            return Ok("Dish updated");
        }
    }
}
