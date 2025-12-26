using DataNotificationOne.Domain.Models;
using DataNotificationOne.Domain.Models.Enums;

namespace DataNotificationOne.Domain.Interfaces.Infra
{
    public interface IAlphaVantageGeneralConsumer
    {
        Task<GeneralResponseModel> TimeSeriesGeneralConsumer(string symbol, FunctionAlphaVantageEnum vantageEnum);
    }
}