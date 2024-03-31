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
        public User OrderedByUser { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderNumber { get; set; }
        public String DeliveryAddress {  get; set; }

        public List<Dish> OrderedDishes { get; set; } = new List<Dish>();
    }
}
