using DataNotificationOne.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNotificationOne.Application.Interfaces
{
    public interface IGetFinanceSummaryVarianceService
    {
        Task<FinanceSummaryDto> GetFinanceSummaryVarianceAsync(string ativo);
    }
}
