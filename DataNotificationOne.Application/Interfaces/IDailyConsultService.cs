using DataNotificationOne.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNotificationOne.Application.Interfaces
{
    public interface IDailyConsultService
    {
        Task<Dictionary<string, AlphaVantageDailyDto>> GetAllDailys(string symbol);

        Task<Dictionary<string, AlphaVantageDailyDto>> GetLastTenDailys(string symbol);

        Task<Dictionary<string, AlphaVantageDailyDto>> GetLastTwentyDailys(string symbol);
    }
}
