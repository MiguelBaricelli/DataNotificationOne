using DataNotificationOne.Domain.Models.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DataNotificationOne.Domain.Interfaces.Infra
{
    public interface ISendGridIntegration
    {
        Task SendEmailAsync(EmailModel emailModel);
    }
}
