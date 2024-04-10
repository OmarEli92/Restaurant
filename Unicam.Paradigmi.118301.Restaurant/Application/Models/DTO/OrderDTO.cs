
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTO
{
    public class OrderDTO
{
        public DateTime OrderDate { get; set; }
        public String DeliveryAddress { get; set; }
        public List<DishDTO> OrderedDishes { get; set; } = new List<DishDTO>();
        public decimal TotalCheck { get; set; }


        public OrderDTO()
        {

        }

        public OrderDTO(Order order)
        {
            OrderDate = order.OrderDate;
            DeliveryAddress = order.DeliveryAddress;
            OrderedDishes = order.OrderedDishes.Select(o => new DishDTO(o)).ToList();
            TotalCheck = order.TotalCheck;
        }
    }
}
