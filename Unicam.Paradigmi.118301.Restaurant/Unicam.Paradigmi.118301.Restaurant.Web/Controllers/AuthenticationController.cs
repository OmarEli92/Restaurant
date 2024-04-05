using Abstractions.Services;
using Application.Abstractions.Services;
using Application.Factories;
using Application.Models.DTO;
using Application.Models.Requests;
using Application.Models.Requests.Users;
using Microsoft.AspNetCore.Mvc;
using Models.Responses.Users;
using System.Security.Claims;

namespace Unicam.Paradigmi._118301.Restaurant.Web.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthenticationController: ControllerBase
    {
        private readonly IUserService userService;
        private readonly IHashingService hashingService;
        private readonly ITokenService tokenService;

        public AuthenticationController(IUserService userService, IHashingService hashingService,
                                        ITokenService tokenService)
        {
            this.userService = userService;
            this.hashingService = hashingService;
            this.tokenService = tokenService;
        }

        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> Registration(RegistrationRequest request)
        {
            var user = request.MaptoEntity();
            await userService.AddUserAsync(user);
            var response = new CreateUserResponse();
            response.User = new UserDTO(user);
            var tokenRequest = new CreateTokenRequest();
            tokenRequest.User = user;
            string token = tokenService.CreateToken(tokenRequest);
            return Ok(ResponseFactory.WithSuccess(token));
            
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
                var tokenRequest = new CreateTokenRequest();
                tokenRequest.User = user;
                string token = tokenService.CreateToken(tokenRequest);

                // return Ok($"Login succesful!! Welcome back {user.FirstName}");
                return Ok(token);
            }
            return BadRequest("Username or password incorrect");

        }

        [HttpGet]
        [Route("Claims")]
        public IActionResult ShowMeClaims()
        {
            var userIdentity = this.User.Identity as ClaimsIdentity;
            var userId = userIdentity.Claims.Where(u => u.Type == "User_id");
            return Ok(userId);
        }
    }
}
