using DataNotificationOne.Application.Dtos;
using DataNotificationOne.Domain.Models;

namespace DataNotificationOne.Application.Interfaces
{
    public interface IDataOverviewService
    {
        Task<OverviewModel> GetAllDataOverviewBySymbolServiceAsync(string symbol);
        Task<SummaryCompanyOverviewDto> GetCompanyOverviewSummaryServiceAsync(string symbol);
    }
}
