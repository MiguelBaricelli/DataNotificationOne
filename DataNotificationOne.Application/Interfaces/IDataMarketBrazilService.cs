using DataNotificationOne.Domain.Models.BraApi;

namespace DataNotificationOne.Application.Interfaces
{
     public interface IDataMarketBrazilService
    {
        Task<BrApiRequest> GetAllBrApiDataAsync(string symbol);
    }
}
