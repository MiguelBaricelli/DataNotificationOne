using DataNotificationOne.Domain.Models;
using DataNotificationOne.Domain.Interfaces.Infra;
namespace DataNotificationOne.Domain.Interfaces.Infra
{
    public interface IAlphaVantageOverviewConsumer
    {
        Task<OverviewModel> GetCompanyOverviewAsync(string symbol);
    }
}
