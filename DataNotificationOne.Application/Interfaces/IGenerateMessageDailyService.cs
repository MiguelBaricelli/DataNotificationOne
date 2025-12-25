using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNotificationOne.Application.Interfaces
{
    public interface IGenerateMessageDailyService
    {
        Task<string> GenerateDailyVarianceMessageAsync(string symbol, DateTime date);
        Task<string> GenerateCustomDailyMessageAsync(string symbol, DateTime date);
        Task<string> GenerateCustomDailyMessageByClientAsync(string nameClient, string symbol, DateTime date);
    }
}
