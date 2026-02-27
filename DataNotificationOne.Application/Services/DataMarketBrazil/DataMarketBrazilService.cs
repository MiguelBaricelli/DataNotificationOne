using DataNotificationOne.Application.Interfaces;
using DataNotificationOne.Domain.Interfaces.Infra.Repository;
using DataNotificationOne.Domain.Models.BraApi;

namespace DataNotificationOne.Application.Services.DataMarketBrazil
{
    public class DataMarketBrazilService : IDataMarketBrazilService
    {
        public readonly IBrApiRepository _brApiRepository;
        public DataMarketBrazilService(IBrApiRepository brApiRepository)
        {
            _brApiRepository = brApiRepository;
        }

        public async Task<BrApiRequest> GetAllBrApiDataAsync(string symbol)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                return new BrApiRequest
                {
                    BraApiResults = new List<BrApiModel>(),
                    RequestedAt = DateTime.UtcNow
                };
            }

                return await _brApiRepository.GetBrApiDataAsync(symbol);
        }
    }
}