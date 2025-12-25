using DataNotificationOne.Domain.Models;

namespace DataNotificationOne.Application.Interfaces
{
    public interface IDataOverviewService
    {
        Task<OverviewModel> GetAllDataOverviewBySymbolServiceAsync(string symbol);
        Task<OverviewModel> GetCompanyOverviewSummaryServiceAsync(string symbol);
    }
}
