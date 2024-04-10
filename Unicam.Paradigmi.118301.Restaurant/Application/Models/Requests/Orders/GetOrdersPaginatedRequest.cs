using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests.Orders
{
    public class GetOrdersPaginatedRequest
{
        
        
        public string DateStart {  get; set; }
        public string DateEnd { get; set; }
        public int? userId { get; set; }
        public string orderBy { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
