using DataNotificationOne.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNotificationOne.Domain.Interfaces.Infra
{
    public interface IAlphaVantageWeeklyConsumer
    {
        Task<FinanceDataModel> GetWeeklyDataAsync(string symbol);
    }
}
