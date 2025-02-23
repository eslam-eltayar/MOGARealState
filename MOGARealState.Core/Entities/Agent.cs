using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGARealState.Core.Entities
{
    public class Agent : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public ICollection<Property> Properties { get; set; } = new HashSet<Property>();
    }
}