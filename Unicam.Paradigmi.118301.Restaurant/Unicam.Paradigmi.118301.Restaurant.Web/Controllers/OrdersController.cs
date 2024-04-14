using Application.Abstractions.Services;
using Application.Factories;
using Application.Models.DTO;
using Application.Models.Requests;
using Application.Models.Requests.Orders;
using Application.Models.Responses.Orders;
using Application.Services;
using Infrastructure.Repositories;
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
        private readonly PdfService pdfService;
        public OrdersController(IOrderService orderService, IUserService userService, PdfService pdfService)
        {
            this.orderService = orderService;
            this.userService = userService;
            this.pdfService = pdfService;
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
        public async Task<IActionResult> GetHistory(GetOrdersPaginatedRequest request)
        {
            var date = DateTime.Now.Date;
            int totalNumberOfOrders = 0;
            var userIdentity = this.User.Identity as ClaimsIdentity;
            var userRole = userIdentity.Claims.Where( u => u.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").First().Value;
            var userId = int.Parse(userIdentity.Claims.Where(u => u.Type == "User_id").First().Value);
            var user = await userService.GetUserAsync(userId);
            List<OrderDTO> history = new List<OrderDTO>();
            switch (userRole)
            {
                case "Customer":
                    history = orderService.GetOrdersFromDateToDate(request.DateStart, request.DateEnd,
                                                                   user.UserId, out totalNumberOfOrders);
                    break;
                case "Admin":
                    history = orderService.GetOrdersFromDateToDate(request.DateStart, request.DateEnd,request.userId,
                                                                     out totalNumberOfOrders);
                    break;
            }
            var pagesFound = (totalNumberOfOrders / (decimal)request.PageSize);
            var pagedHistory = history
                .Skip(request.PageNumber * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var response = new GetPaginatedHistoryResponse(); 
            response.orders = pagedHistory;
            response.NumberOfPages = (int)Math.Ceiling(pagesFound);
            return Ok(ResponseFactory.WithSuccess(response));
           
        }

        [HttpPost]
        [Route("DownloadHistory")]
        public IActionResult DownloadHistory(GetOrdersPaginatedRequest request)
        {
            try { 
                    int totalNumberOfOrders = 0;
                    List<OrderDTO> orders = orderService.GetOrdersFromDateToDate(request.DateStart, request.DateEnd, request.userId, out totalNumberOfOrders);
                    byte[] pdfBytes = pdfService.GeneratePdfFromOrders(orders);
                    if (pdfBytes != null && pdfBytes.Length > 0)
                    {
                        return File(pdfBytes, "application/pdf", "orders.pdf");
                    }
                    else
                    {
                        return BadRequest("Unable to generate PDF.");
                    }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        private  int getUserIdFromIdentity()
        {
            var userIdentity = this.User.Identity as ClaimsIdentity;
            return int.Parse(userIdentity.Claims
                .Where(c => c.Type == "User_id")
                .First().Value);
        }
    }

    
}
