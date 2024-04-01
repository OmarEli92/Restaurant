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

        List<Order> GetOrders(int start, string? attribute, User? user,int num, out int totalNumberOfOrders);
        Task<Order> GetOrderAsync(int id);
        List<Order> GetOrdersByUser(int start, string? attribute, User? user, int num, out int totalNumberOfOrders);

        int AddOrderAsync(Order order, out decimal totalCheck);

        Task RemoveOrderAsync(Order order);

        Task UpdateOrderAsync(Order order);


    }
}
