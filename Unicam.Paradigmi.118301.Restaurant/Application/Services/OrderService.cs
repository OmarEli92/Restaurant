using Application.Abstractions.Services;
using Application.Models.Requests.Orders;
using Infrastructure.Repositories;
using Models.Entities;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Application.Services
{
    public class OrderService : IOrderService


    {
        private readonly OrderRepository orderRepository;
        private readonly DishRepository dishRepository;
        public OrderService(OrderRepository orderRepository, DishRepository dishRepository)
        {
            this.orderRepository = orderRepository;
            this.dishRepository = dishRepository;
        }

        
        public int AddOrder(Order order, out decimal totalCheck)
        {

            // if the selected dishes are in the menu then the order is valid
            if (order.OrderedDishes.All(d => CheckIfDishExists(d))) 
            { 
            order.TotalCheck = CalculateTheCheck(order,(decimal) 0.9);
            var orderId = orderRepository.AddOrder(order, out totalCheck);
            return orderId;
            }
            //otherwise return -1 as id
            totalCheck = 0;
            return -1;
        }

        public Order GenerateOrder(List<String> dishesOrdered)
        {
            var dishes = getAllDishesFromRequest(dishesOrdered);
            var order = new Order();
            order.OrderDate = DateTime.Now;
            order.OrderedDishes = dishes;
            return order;
        }
        private List<Dish> getAllDishesFromRequest(List<String> dishesRequest)
        {
            var dishes = new List<Dish>();
            var order = new Order(); //TODO VERIFICARE E ELIMINARE SE RIDONDANTE
            return dishesRequest.Select(d => dishRepository.GetDishByName(d)).ToList();
            
        }

        private bool CheckIfDishExists(Dish dish)
        {
            var checkedDish = dishRepository.GetDishAsync(dish.DishId);
            if (checkedDish != null) return true;
            return false;
        }
        private bool CheckForDiscount(Order order)
        {
            var types = new HashSet<MenuCourses>() { MenuCourses.Starter,MenuCourses.MainCourse,
                                                    MenuCourses.SideDish, MenuCourses.Dessert};
            // check if the order is complete 
            return types.All(type => order.OrderedDishes.Any(d => d.Type == type)); 
        }
        private decimal CalculateTheCheck(Order order, decimal discountPercentage)
        {
            if (!CheckForDiscount(order))
            {
                return order.OrderedDishes.Sum(d => d.Price);
            }
            var mostExpensiveStarter = order.OrderedDishes
                                        .Where(d => d.Type == MenuCourses.Starter)
                                        .OrderByDescending(d => d.Price)
                                        .First();
            // apply the discount (10%) to the orderedDishes and the most expensive Starter +             
            var totalCheck =  (order.OrderedDishes
                                    .Where(d => d.Type != MenuCourses.Starter)
                                    .Sum(d => d.Price) + mostExpensiveStarter.Price) * (discountPercentage)
                                    + order.OrderedDishes
                                    .Where(d => d.Type == MenuCourses.Starter && d.Type != mostExpensiveStarter.Type)
                                    .Sum(d => d.Price);
            return totalCheck;
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
