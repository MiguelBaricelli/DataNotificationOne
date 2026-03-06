using DataNotificationOne.Domain.Interfaces.Infra;
using DataNotificationOne.Domain.Interfaces.Infra.Repository;
using DataNotificationOne.Domain.Models.BraApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNotificationOne.Infrastructure.Repository
{
    public class BrApiRepository : IBrApiRepository
    {
        public readonly IBrApiIntegrationConsumer _brApiIntegrationConsumer;
        public BrApiRepository(IBrApiIntegrationConsumer brApiIntegrationConsumer)
        {
            _brApiIntegrationConsumer = brApiIntegrationConsumer;

        }

        public async Task<BrApiRequest> GetBrApiDataAsync(string symbol)
        {
            return await _brApiIntegrationConsumer.GetBrapiDataAsync(symbol);
        }
    }
}
