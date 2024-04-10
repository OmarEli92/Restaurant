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
       
        public string DeliveryAddress { get; set; }
        public List<DishDTO> OrderedDishes { get; set; }

        public Order MapToEntity()
        {
            var order = new Order();
            order.DeliveryAddress = DeliveryAddress;
            return order;
        }
    }
}
