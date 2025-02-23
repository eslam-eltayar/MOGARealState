using System.Runtime.Serialization;

namespace MOGARealState.Core.Enums
{
    public enum PropertyType
    {
        [EnumMember(Value = "Apartment")]
        Apartment,
        [EnumMember(Value = "House")]
        House,
        [EnumMember(Value = "Office")]
        Office,
        [EnumMember(Value = "Store")]
        Store,
        [EnumMember(Value = "Villa")]
        Villa
    }
}
