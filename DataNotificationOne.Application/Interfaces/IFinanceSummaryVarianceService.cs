using DataNotificationOne.Application.Dtos;
using DataNotificationOne.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNotificationOne.Application.Interfaces
{
    public interface IFinanceSummaryVarianceService
    {
        Task<Dictionary<string, FinanceSummaryDto>> GetFinanceSummaryVarianceAsync(string ativo, DateTime date);

        public decimal AssetVariation(decimal close, decimal open);
    }
}
