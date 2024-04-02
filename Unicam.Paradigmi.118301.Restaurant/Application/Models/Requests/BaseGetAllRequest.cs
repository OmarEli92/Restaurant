using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    /// <summary>
    /// In this simple scenario where only three entities are involved and pagination is the same for all of them
    /// one simple class can be used as a basic GetAll request , in other cases a more specific for type class 
    /// it should be used instead
    /// </summary>
    public class BaseGetAllRequest
{
        //the index of the page
        public int PageNumber { get; set; }
        //page size 
        public int PageSize { get; set; }
    }
}
