using Application.Abstractions.Services;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;

namespace Unicam.Paradigmi._118301.Restaurant.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController: ControllerBase
    {
        private readonly IUserService userService;
        
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        
        
        [HttpPost]
        [Route("all")]
        public IEnumerable<User> GetUsers(int start,int num)
        {
            int totalNumber = 0;
            return userService.GetUsers(start, num, out totalNumber);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddUser(User user) {

               await userService.AddUserAsync(user);
            return Ok($"User {user.Email} added");
            
        }
    }
}
