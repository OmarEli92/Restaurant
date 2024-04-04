using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Services
{
    public interface IDishService
    {
        IEnumerable<Dish> GetDishes(int start, int num,string? attribute, out int totalNumberOfDishes);
        Task<Dish> GetDishByIdAsync(int id);

        Task AddDishAsync(Dish dish);

        Task RemoveDishByIdAsync(int dishId);

        Task UpdateDishAsync(Dish dish);
        Dish GetDishByName(string name);



    }
}
