using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGARealState.Core.DTOs.Requests
{
    public class AllPropertiesResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HeadImage { get; set; }
        public decimal Price { get; set; }
    }
}
