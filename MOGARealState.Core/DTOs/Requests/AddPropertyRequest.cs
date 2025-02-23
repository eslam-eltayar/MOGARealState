using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGARealState.Core.DTOs.Requests
{
    public record AddPropertyRequest
    (
        string Name,
        string Description,
        string Location,
        int Size,
        decimal Price,
        int RoomsCount,
        int BathroomsCount,
        int FloorsCount,

        IFormFile HeadImage,
        IFormFile Image1,
        IFormFile Image2,
        IFormFile Image3,
        IFormFile? Video,
        string Purpose,
        string Type,
        string Status,
        bool HasParking,
        bool HasWifi,
        bool HasElevator,
        bool IsFurnished,
        int AgentId
       
        );
}
