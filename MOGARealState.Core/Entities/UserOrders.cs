using MOGARealState.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGARealState.Core.Entities
{
    public class UserOrders : BaseEntity
    {
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }

        public Property Property { get; set; }
        public int PropertyId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public OrderStatus Status { get; set; }
    }
}
