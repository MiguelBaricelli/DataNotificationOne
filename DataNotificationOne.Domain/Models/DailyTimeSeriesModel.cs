using DataNotificationOne.Domain.Services;
using System.Text.Json.Serialization;

namespace DataNotificationOne.Domain.Models
{
    public class DailyTimeSeriesModel
    {
        [JsonPropertyName("Time Series (Daily)")]
        public Dictionary<string, AlphaVantageDailyDto> TimeSeriesDaily { get; set; }
    }

}
