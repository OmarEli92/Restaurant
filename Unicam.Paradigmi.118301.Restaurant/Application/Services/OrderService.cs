using Application.Abstractions.Services;
using Application.Models.DTO;
using Application.Models.Requests.Orders;
using Application.Models.Responses.Orders;
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
            order.TotalCheck = CalculateTheCheck(order,(decimal) 0.9);
            var orderId = orderRepository.AddOrder(order, out totalCheck);
            return orderId;  
        }

        public Order GenerateOrder(List<DishDTO> dishesOrdered, int orderId, int userId)
        {
            
            var order = new Order();
            var dishes = GenerateTheListOfDishesFromDTO(dishesOrdered, orderId);     
            order.OrderDate = DateTime.Now;
            order.OrderedDishes = dishes;
            order.UserId = userId;
            return order;
        }
        private List<Dish> GenerateTheListOfDishesFromDTO(List<DishDTO> dishesDTO, int orderId)
        {
            return dishesDTO
                .Select<DishDTO, Dish>(d =>
                {
                    var dish = new Dish();
                    dish.OrderId = orderId;
                    dish.Name = d.Name;
                    dish.Quantity = d.Quantity;
                    dish.Price = d.Price;
                    dish.Type = d.Type;
                    return dish;
                })
                .ToList();
        }
        private bool CheckForDiscount(Order order)
        {
            var types = new HashSet<MenuCourses>() { MenuCourses.Starter,MenuCourses.MainCourse,
                                                    MenuCourses.SideDish, MenuCourses.Dessert};
            // check if the order is complete 
            return types.All(type => order.OrderedDishes.Any(d => d.Type == type)); 
        }

        /* The discount is only applied to one and only dish for each courses if the menu is complete
          the most expensive one get the discount the others are at full price*/
        private decimal CalculateTheCheck(Order order, decimal discountPercentage)
        {
            if (!CheckForDiscount(order))
            {
                return order.OrderedDishes.Sum(d => d.Price);
            }
            var mostExpensiveStarter = GetMostExpensiveDishForEachType(order, MenuCourses.Starter);
            var mostExpensiveMainCourse = GetMostExpensiveDishForEachType(order, MenuCourses.MainCourse);
            var mostExpensiveSideCourse = GetMostExpensiveDishForEachType(order, MenuCourses.SideDish);
            var mostExpensiveDessert = GetMostExpensiveDishForEachType(order, MenuCourses.Dessert);
            decimal totalCheck = 0;
            order.OrderedDishes.ForEach(dish =>
            {
                decimal price = dish.Price;
                if (dish.Equals(mostExpensiveStarter) || dish.Equals(mostExpensiveMainCourse)
                               || dish.Equals(mostExpensiveSideCourse) || dish.Equals(mostExpensiveDessert))
                {
                    price *= discountPercentage;
                    // In the case the most expensive dish of the course has a quantity greater than 1
                    if (CheckIfQuantityIsGreaterThanOneInExpensiveCourses(dish))
                    {
                        
                        price += (dish.Quantity - 1) * dish.Price;
                    }
                    
                }
                totalCheck += price;
            });
            return totalCheck;
        }

        private bool CheckIfQuantityIsGreaterThanOneInExpensiveCourses(Dish expensiveDish)
        {
            return expensiveDish.Quantity > 1;
        }
        private Dish GetMostExpensiveDishForEachType(Order order,MenuCourses type)
        {
            return order.OrderedDishes
                .Where(d => d.Type == type)
                .OrderByDescending(d => d.Price)
                .First();
        }
        public async Task<Order> GetOrderAsync(int id)
        {
            return await orderRepository.GetOrderAsync(id);
        }

        public List<OrderDTO> GetOrdersFromUser(int start,string? attribute,User? user,
                                            int num, out int totalNumberOfOrders)
        {
            var orders =  orderRepository.GetOrdersFromUser(start, num,attribute,user,out totalNumberOfOrders);
            var ordersResponse = new List<OrderDTO>();
            foreach(Order order in orders)
            {
                ordersResponse.Add(new OrderDTO(order));
            }
            return ordersResponse;
        }

       

        public async Task RemoveOrderAsync(Order order)
        {
            await  orderRepository.RemoveOrderAsync(order);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            await orderRepository.UpdateOrderAsync(order);
        }

        public int GenerateID()
        {
            var lastOrder = orderRepository.GetAll()
                .OrderBy(o => o.OrderID)
                .Reverse()
                .FirstOrDefault();
            if(lastOrder == null)
            {
                return 0;
            }
            return lastOrder.OrderID + 1;
        }

        
    }
}
