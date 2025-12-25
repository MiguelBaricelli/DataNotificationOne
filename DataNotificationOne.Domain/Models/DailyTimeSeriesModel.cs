using DataNotificationOne.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataNotificationOne.Domain.Models
{
    public class DailyTimeSeriesModel
    {
        [JsonPropertyName("Daily Time Series")]
        public Dictionary<string, AlphaVantageDailyDto> DailyTimeSeries { get; set; }
    }

}
