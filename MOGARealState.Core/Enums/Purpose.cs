using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MOGARealState.Core.Enums
{
    public enum Purpose
    {
        [EnumMember(Value = "ForRent")]
        ForRent,
        [EnumMember(Value = "ForSale")]
        ForSale
    }
}
