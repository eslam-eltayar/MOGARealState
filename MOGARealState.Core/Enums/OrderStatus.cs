using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MOGARealState.Core.Enums
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,

        [EnumMember(Value ="InProcess")]
        InProcess,

        [EnumMember(Value ="Canceled")]
        Canceled,

        [EnumMember(Value = "Confirmed")]
        Confirmed,
    }
}
