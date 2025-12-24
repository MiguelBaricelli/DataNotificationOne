using DataNotificationOne.Domain.Models;

namespace DataNotificationOne.Application.Interfaces
{
    public interface IGetWeeklyDataForConsultService
    {
        Task<WeeklyTimeSeriesModel> GetWeeklyDataAsync(string symbol);
        Task<WeeklyTimeSeriesModel> GetDataByWeekly(string symbol, DateTime date);
        Task<WeeklyTimeSeriesModel> GetLastTenWeeklys(string symbol);
    }
}
