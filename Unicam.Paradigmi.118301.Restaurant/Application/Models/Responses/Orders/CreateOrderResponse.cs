using Application.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Responses.Orders
{
    public class CreateOrderResponse
    {
        public OrderDTO Order { get; set; } = null!;
    }
}
