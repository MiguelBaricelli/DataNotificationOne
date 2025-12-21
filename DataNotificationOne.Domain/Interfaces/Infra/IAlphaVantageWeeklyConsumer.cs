using DataNotificationOne.Domain.Models;

namespace DataNotificationOne.Domain.Interfaces.Infra
{
    public interface IAlphaVantageWeeklyConsumer
    {
        Task<FinanceDataModel> GetWeeklyDataAsync(string symbol);
    }
}
