using DataNotificationOne.Domain.Models;
using DataNotificationOne.Domain.Services;

namespace DataNotificationOne.Infrastructure.Interfaces
{
    public interface IAlphaVantageClient
    {
        Task<FinanceDataModel> GetFinanceDataAsync(string ativo);
    }
}
