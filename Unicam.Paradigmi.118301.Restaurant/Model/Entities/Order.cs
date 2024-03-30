using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    /**This class represents an order made by a customer **/
    public class Order
    {
        private User OrderedByUser { get; set; }
        private DateTime OrderDate { get; set; }
        private int OrderNumber { get; set; }
        private String DeliveryAddress {  get; set; }

        private List<Dish> OrderedDishes { get; set; } = new List<Dish>();
    }
}
