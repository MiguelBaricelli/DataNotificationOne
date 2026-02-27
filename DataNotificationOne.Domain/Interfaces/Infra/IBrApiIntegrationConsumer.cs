using DataNotificationOne.Domain.Models.BraApi;

namespace DataNotificationOne.Domain.Interfaces.Infra
{
    public interface IBrApiIntegrationConsumer 
    {
        Task<BrApiRequest> GetBrapiDataAsync(string symbol);
    }
}
