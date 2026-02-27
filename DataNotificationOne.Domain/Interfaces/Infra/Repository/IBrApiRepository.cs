using DataNotificationOne.Domain.Models.BraApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNotificationOne.Domain.Interfaces.Infra.Repository
{
    public interface IBrApiRepository
    {
        Task<BrApiRequest> GetBrApiDataAsync(string symbol);
    }
}
