using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGARealState.Core.Entities
{
    public class FavoriteUserProperties : BaseEntity
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public int PropertyId { get; set; }
        public Property Property { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
