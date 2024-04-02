using Application.Abstractions.Services;
using Infrastructure.Repositories;
using Models.Entities;

namespace Application.Services
{
    public class DishService : IDishService
    {
        private readonly DishRepository dishRepository;

        public DishService(DishRepository dishRepository)
        {
            this.dishRepository = dishRepository;
        }


        //TODO da implementare
        public async Task AddDishAsync(Dish dish)
        {
            await dishRepository.AddDishAsync(dish);
        }

        public async Task<Dish> GetDishByIdAsync(int id)
        {
            return await dishRepository.GetDishAsync(id);
        }

        public  IEnumerable<Dish> GetDishes(int start, int num,string? attribute, out int totalNumberOfDishes)
        {
            return dishRepository.GetDishes(start, num,attribute, out totalNumberOfDishes); 
        }

        public async Task RemoveDishByIdAsync(int dishID)
        {
            await dishRepository.DeleteDishByIdAsync(dishID);
        }

        public async Task UpdateDishAsync(Dish dish)
        {
            await dishRepository.UpdateDishAsync(dish);
        }


    }
}
