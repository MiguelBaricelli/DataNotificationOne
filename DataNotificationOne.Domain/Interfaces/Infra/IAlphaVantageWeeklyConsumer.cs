using DataNotificationOne.Domain.Models;
using DataNotificationOne.Domain.Services;

namespace DataNotificationOne.Domain.Interfaces.Infra
{
    public interface IAlphaVantageWeeklyConsumer
    {
        Task<WeeklyTimeSeriesModel> GetWeeklyDataAsync(string symbol);
    }
}
