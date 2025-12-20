using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataNotificationOne.Domain.Services
{
    public class AlphaVantageResponseDto
    {
        [JsonPropertyName("Time Series (Daily)")]
        public Dictionary<string, AlphaVantageDailyDto> TimeSeries { get; set; }
    }
}
