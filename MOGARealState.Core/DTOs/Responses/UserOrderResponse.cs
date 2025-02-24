using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGARealState.Core.DTOs.Responses
{
    public class UserOrderResponse
    {
        public int OrderId { get; set; }
        public int PropertyId { get; set; }

        public string UserId { get; set; }

        public string PropertyName { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }

        public DateTime Date { get; set; }
        public string Status { get; set; }

        public string Phone { get; set; }
    }

}
