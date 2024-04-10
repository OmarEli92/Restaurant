using Application.Models.DTO;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Services
{
    public interface IOrderService
    {

       
        Task<Order> GetOrderAsync(int id);
        List<OrderDTO> GetOrdersFromUser(int start, string? attribute, User? user, int num, out int totalNumberOfOrders);

        int AddOrder(Order order, out decimal totalCheck);

        Task RemoveOrderAsync(Order order);

        Task UpdateOrderAsync(Order order);

        Order GenerateOrder(List<DishDTO> dishesOrdered,int orderId, int userId);

        int GenerateID();
    }
}
