using Application.Abstractions.Services;
using Infrastructure.Repositories;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    internal class OrderService : IOrderService

    {
        private readonly OrderRepository orderRepository;

        public OrderService(OrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public int AddOrderAsync(Order order, out decimal totalCheck)
        {

            totalCheck =  orderRepository.AddOrder(order, out totalCheck);
            return order.OrderID;
        }

        public async Task<Order> GetOrderAsync(int id)
        {
            return await orderRepository.GetOrderAsync(id);
        }

        public List<Order> GetOrdersByUser(int start,string? attribute,User? user,
                                            int num, out int totalNumberOfOrders)
        {
            return orderRepository.GetOrders(start, num,attribute,user,out totalNumberOfOrders);
        }

        public List<Order> GetOrders(int start, string? attribute, User? user,
                                            int num, out int totalNumberOfOrders)
        {
            return orderRepository.GetOrders(start, num,attribute,user, out totalNumberOfOrders);
        }

        public async Task RemoveOrderAsync(Order order)
        {
            await  orderRepository.RemoveOrderAsync(order);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            await orderRepository.UpdateOrderAsync(order);
        }

        
    }
}
