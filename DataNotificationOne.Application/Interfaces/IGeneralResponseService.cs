using DataNotificationOne.Domain.Models;
using DataNotificationOne.Domain.Models.Enums;

namespace DataNotificationOne.Application.Interfaces
{
    public interface IGeneralResponseService
    {
        Task<GeneralResponseModel> GeneralResponseServiceAsync(string symbol, DateTime date, FunctionAlphaVantageEnum vantageEnum);
    }
}
