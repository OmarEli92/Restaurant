using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests.Users

{
    public class GetUsersRequest
{
        //the index of the page
        public int StartingIndex;
        //page size 
        public int PageSize;
}
}
