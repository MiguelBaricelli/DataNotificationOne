using DataNotificationOne.Domain.Models;

namespace DataNotificationOne.Domain.Interfaces.Infra
{
    public interface IAlphaVantageDailyConsumer
    {
        Task<DailyTimeSeriesModel> TimeSeriesDailyConsumer(string ativo);
    }
}
