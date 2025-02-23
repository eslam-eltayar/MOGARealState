using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MOGARealState.Core.Enums
{
    public enum PropertyStatus
    {
        [EnumMember(Value = "Available")]
        Available,
        [EnumMember(Value = "Sold")]
        Sold,
        [EnumMember(Value = "Rented")]
        Rented
    }
}
