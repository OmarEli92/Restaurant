using Application.Models.DTO;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests.Orders
{
    public class AddOrderRequest
    {
        //public User OrderedByUser { get; set; }
        //public DateTime OrderDate { get; set; }
        public string DeliveryAddress { get; set; }
        //public List<Dish> OrderedDishes { get; set; }
        public List<String> OrderedDishes { get; set; }
        //public decimal TotalCheck { get; set; }

        public Order MapToEntity()
        {
            var order = new Order();
            //order.OrderedByUser = OrderedByUser;
            //order.OrderDate = OrderDate;
            order.DeliveryAddress = DeliveryAddress;
            //order.OrderedDishes = OrderedDishes;
            //order.TotalCheck = TotalCheck;
            return order;
        }
    }
}
