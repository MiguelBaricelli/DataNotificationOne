using DataNotificationOne.Domain.Models.Email;

namespace DataNotificationOne.Domain.Interfaces.Infra
{
    public interface ISendGridIntegration
    {
        Task<bool> SendEmailAsync(EmailModel emailModel);
    }
}
