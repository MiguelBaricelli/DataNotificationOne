using DataNotificationOne.Domain.Models.Email;

namespace DataNotificationOne.Application.Interfaces
{
    public interface IGenerateMessageDailyService
    {
        Task<EmailModel> GenerateGenericDailyMessageAsync(string asset, DateTime date, string toEmail);
        Task<EmailModel> GenerateDailyVarianceMessageAsync(string clientName, string asset, DateTime date, string toEmail);
        Task<EmailModel> GenerateCustomDailyEmailByClientAsync(string clientName, string asset, DateTime date, string toEmail);
    }
}
