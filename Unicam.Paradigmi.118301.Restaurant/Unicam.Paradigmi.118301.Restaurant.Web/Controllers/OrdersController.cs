using Application.Abstractions.Services;
using Application.Factories;
using Application.Models.DTO;
using Application.Models.Requests;
using Application.Models.Requests.Orders;
using Application.Models.Responses.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using System.Security.Claims;

namespace Unicam.Paradigmi._118301.Restaurant.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(Roles="Customer,Admin")]
    public class OrdersController: ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IUserService userService;
        public OrdersController(IOrderService orderService, IUserService userService)
        {
            this.orderService = orderService;
            this.userService = userService;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddOrder(AddOrderRequest orderRequests)
        {
            int orderId = orderService.GenerateID();
            // Get the customerID from the active user
            var activeUser = this.User.Identity as ClaimsIdentity;
            int userId = int.Parse(activeUser.Claims.Where(u => u.Type == "User_id").First().Value);
            var order = orderService.GenerateOrder(orderRequests.OrderedDishes, orderId, userId);
            order.DeliveryAddress = orderRequests.DeliveryAddress;
            // set the info of the customer who is makind the order
            order.User = await userService.GetUserAsync(userId);
            decimal TotalCheck = 0;
            orderService.AddOrder(order, out TotalCheck);
            var response = new AddOrderResponse();
            response.Order = new Application.Models.DTO.OrderDTO(order);
            return Ok(ResponseFactory.WithSuccess(response));
        }


        // Returns the list of all the customer's orders if the active user is a customer,
        // in the case the active user is the admin it returns all the orders created by every customer.
        [HttpPost]
        [Route("History")]
        public async Task<IActionResult> GetHistory(BaseGetAllRequest request)
        {
            int totalNumberOfOrder = 0;
            var userIdentity = this.User.Identity as ClaimsIdentity;
            var userRole = userIdentity.Claims.Where( u => u.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").First().Value;
            var userId = int.Parse(userIdentity.Claims.Where(u => u.Type == "User_id").First().Value);
            var user = await userService.GetUserAsync(userId);
            List<OrderDTO> history = new List<OrderDTO>();
            switch (userRole)
            {
                case "Customer":
                    history = orderService.GetOrdersFromUser(request.PageNumber, request.OrderByAttribute,
                                                    user, request.PageSize, out totalNumberOfOrder);
                    break;
                case "Admin":
                    history = orderService.GetOrdersFromUser(request.PageNumber, request.OrderByAttribute,
                                                    user, request.PageSize, out totalNumberOfOrder);
                    break;
            }
            return Ok(ResponseFactory.WithSuccess(history));
        }

        
        
    }
}
