using MOGARealState.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGARealState.Core.Entities
{
    public class Property : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int Size { get; set; } = 0;
        public decimal Price { get; set; } = 0m;
        public int RoomsCount { get; set; } = 0;
        public int BathroomsCount { get; set; } = 0;
        public int FloorsCount { get; set; } = 0;
        public bool IsFurnished { get; set; }


        public string HeadImage { get; set; } = string.Empty;
        public string Image1 { get; set; } = string.Empty;
        public string Image2 { get; set; } = string.Empty;
        public string Image3 { get; set; } = string.Empty;
        public string? VideoUrl { get; set; }


        public Purpose Purpose { get; set; }
        public PropertyType Type { get; set; }
        public PropertyStatus Status { get; set; }

        public bool HasParking { get; set; }
        public bool HasWifi { get; set; }
        public bool HasElevator { get; set; }

        public Agent Agent { get; set; } // Navigation Property
        public int AgentId { get; set; } // FK

    }
}
