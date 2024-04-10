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
        public int OrderID { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime OrderDate { get; set; }
        
        public string DeliveryAddress {  get; set; }

        public List<Dish> OrderedDishes { get; set; } = new List<Dish>();
        public decimal TotalCheck {  get; set; }
    }
}
