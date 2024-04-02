using Application.Abstractions.Services;
using Application.Models.Requests.Users;
using Microsoft.AspNetCore.Mvc;
using Model.Responses.Users;
using Models.Responses.Users;

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
        public IActionResult GetUsers(GetUsersRequest request)
        {
            int totalNumber = 0;
            var users = userService.GetUsers(request.StartingIndex * request.PageSize, request.PageSize, out totalNumber);
            decimal pages = (totalNumber / (decimal) request.PageSize);
            var response = new GetUsersResponse();
            response.Users = users.Select(u => new Application.Models.DTO.UserDTO(u)).ToList();
            response.NumberOfPages = (int)Math.Ceiling(pages);
            return Ok(response);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddUser(CreateUserRequest request) {
            var user = request.MaptoEntity();
            await userService.AddUserAsync(user);
            var response = new CreateUserResponse();
            response.User = new Application.Models.DTO.UserDTO(user);
            return Ok(response);
        }
    }
}
