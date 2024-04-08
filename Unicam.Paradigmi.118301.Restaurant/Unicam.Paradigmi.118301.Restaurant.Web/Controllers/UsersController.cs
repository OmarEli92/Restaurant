using Application.Abstractions.Services;
using Application.Factories;
using Application.Models.DTO;
using Application.Models.Requests;
using Application.Models.Requests.Users;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Responses.Users;
using Models.Responses.Users;

namespace Unicam.Paradigmi._118301.Restaurant.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(Roles ="Admin")]
    public class UsersController: ControllerBase
    {
        private readonly IUserService userService;
        private readonly ITokenService tokenService;
        
        public UsersController(IUserService userService, ITokenService tokenService)
        {
            this.userService = userService;
            this.tokenService = tokenService;
        }

        
        
        [HttpPost]
        [Route("all")]
        public IActionResult GetUsers(BaseGetAllRequest request)
        {
            int totalNumber = 0;
            var users = userService.GetUsers(request.PageNumber * request.PageSize, request.PageSize, out totalNumber);
            var pages = (totalNumber / (decimal) request.PageSize);
            var response = new GetUsersResponse();
            response.Users = users.Select(u => new Application.Models.DTO.UserDTO(u)).ToList();
            response.NumberOfPages = (int)Math.Ceiling(pages);
           
            return Ok(ResponseFactory.WithSuccess(response));
 
        }

        [HttpPost]
        [Route("Remove/{id}")]
        public async Task<IActionResult> RemoveUser(int id) {
            await userService.RemoveUserAsync(id);
            return Ok(ResponseFactory.WithSuccess("User removed"));
        }

        [HttpPost]
        [Route("Admin/add")]
        public async Task<IActionResult> AddAdmin(RegistrationRequest request)
        {
            var user = request.MaptoEntity();
            user.Role = "Admin";
            await userService.AddUserAsync(user);
            var response = new CreateUserResponse();
            response.User = new UserDTO(user);
            var tokenRequest = new CreateTokenRequest();
            tokenRequest.User = user;
            string token = tokenService.CreateToken(tokenRequest);
            return Ok(ResponseFactory.WithSuccess(token));
        }
    }
}
