using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGARealState.Core.DTOs.Responses
{
    public class PropertyResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public double Size { get; set; }
        public decimal Price { get; set; }
        public int RoomsCount { get; set; }
        public int BathroomsCount { get; set; }
        public bool HasParking { get; set; }
        public bool HasWifi { get; set; }
        public bool HasElevator { get; set; }
        public string Purpose { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string HeadImage { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string? VideoUrl { get; set; }

        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public string AgentEmail { get; set; }
        public string AgentPhone { get; set; }
        public string? AgentImage { get; set; }

    }
}