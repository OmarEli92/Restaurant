using Abstractions.Services;
using Application.Abstractions.Services;
using Application.Factories;
using Application.Models.DTO;
using Application.Models.Requests.Users;
using Microsoft.AspNetCore.Mvc;
using Models.Responses.Users;

namespace Unicam.Paradigmi._118301.Restaurant.Web.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthenticationController: ControllerBase
    {
        private readonly IUserService userService;
        private readonly IHashingService hashingService;

        public AuthenticationController(IUserService userService, IHashingService hashingService)
        {
            this.userService = userService;
            this.hashingService = hashingService;
        }

        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> Registration(RegistrationRequest request)
        {
            var user = request.MaptoEntity();
            await userService.AddUserAsync(user);
            var response = new CreateUserResponse();
            response.User = new UserDTO(user);
            return Ok(ResponseFactory.WithSuccess(response));
            
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await userService.GetUserByEmailAsync(request.Email);
            if(user  == null)
            {
                return BadRequest("No user with such email found");
            }
            //checking if the password provided is correct
            if(hashingService.VerifyPassword(request.Password, user.Salt, user.Password))
            {
                return Ok($"Login succesful!! Welcome back {user.FirstName}");
            }
            return BadRequest("Username or password incorrect");

        }
    }
}
