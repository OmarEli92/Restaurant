using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    /** This class represents a dish that can be ordered by a customer**/
    public class Dish
    {
        private string name {  get; set; }
        private string description { get; set; }
        private string price { get; set; }
        private MenuCourses type { get; set; }
    }
}
