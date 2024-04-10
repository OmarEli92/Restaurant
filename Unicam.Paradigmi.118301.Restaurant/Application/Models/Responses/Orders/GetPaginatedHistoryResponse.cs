
using Application.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Responses.Orders
{
    public class GetPaginatedHistoryResponse
{
        public List<OrderDTO> orders;

        public int NumberOfPages { get; set; }
    }
}
