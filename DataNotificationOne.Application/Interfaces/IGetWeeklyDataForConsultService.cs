using DataNotificationOne.Domain.Models;

namespace DataNotificationOne.Application.Interfaces
{
    public interface IGetWeeklyDataForConsultService
    {
        Task<FinanceDataModel> GetWeeklyDataAsync(string symbol);
        Task<FinanceDataModel> GetDataByWeekly(string symbol, DateTime date);
    }
}
