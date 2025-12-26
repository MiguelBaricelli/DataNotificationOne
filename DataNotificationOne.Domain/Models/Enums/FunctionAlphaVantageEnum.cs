using System.Runtime.Serialization;

namespace DataNotificationOne.Domain.Models.Enums
{

    public enum FunctionAlphaVantageEnum
    {
        NONE = 0,

        [EnumMember(Value = "DAILY")]
        TIME_SERIES_DAILY,

        [EnumMember(Value = "WEEKLY")]
        TIME_SERIES_WEEKLY,

        [EnumMember(Value = "MONTHLY")]
        TIME_SERIES_MONTHLY
    }

}
