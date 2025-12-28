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
        Task<string> GenerateGenericDailyMessageAsync(string symbol, DateTime date);
        Task<string> GenerateCustomDailyEmailByClientAsync(string nameClient, string symbol, DateTime date);
    }
}
