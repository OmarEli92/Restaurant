using Application.Abstractions.Services;
using Application.Factories;
using Application.Models.Requests.Orders;
using Application.Models.Responses.Orders;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;

namespace Unicam.Paradigmi._118301.Restaurant.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrdersController: ControllerBase
    {
        private readonly IOrderService orderService;
        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult AddOrder(AddOrderRequest orderRequests)
        {
            var order = orderService.GenerateOrder(orderRequests.OrderedDishes);
            order.DeliveryAddress = orderRequests.DeliveryAddress;

            //TODO IMPOSTARE L'UTENTE CHE ESEGUE L'ORDINE DOPO AVER AGGIUNTO JWT E AUTENTICAZIONE
            decimal TotalCheck = 0;
            orderService.AddOrder(order, out TotalCheck);
            var response = new AddOrderResponse();
            response.Order = new Application.Models.DTO.OrderDTO(order);
            return Ok(ResponseFactory.WithSuccess(response));
            /*
            var order = request.MapToEntity();
            decimal TotalCheck = 0;
            orderService.AddOrder(order, out TotalCheck);
            var response = new AddOrderResponse();
            response.Order = new Application.Models.DTO.OrderDTO(order);
            return Ok(ResponseFactory.WithSuccess(response));
            */
            return Ok();
        }
        
    }
}
