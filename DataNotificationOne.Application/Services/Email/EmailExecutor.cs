using DataNotificationOne.Application.Dtos;
using DataNotificationOne.Application.Interfaces;
using DataNotificationOne.Domain.Interfaces.Infra;
using Microsoft.Extensions.Logging;

namespace DataNotificationOne.Application.Services.Email
{
    public class EmailExecutor : IEmailExecutor
    {
        IGenerateMessageDailyService _generateMessageDaily;
        ISendGridIntegration _sendGridIntegraion;
        ILogger<EmailExecutor> _logger;

        public EmailExecutor(IGenerateMessageDailyService generateMessageDaily,
            ISendGridIntegration sendGridIntegration,
            ILogger<EmailExecutor> logger)
        {
            _generateMessageDaily = generateMessageDaily;
            _sendGridIntegraion = sendGridIntegration;
            _logger = logger;
        }

        public async Task<bool> ExecuteEmailDailyAsync(InputEmailGenericDailyDto emailGenericDailyDto)
        {
            if (string.IsNullOrWhiteSpace(emailGenericDailyDto.Asset))
            {
                return false;
            }
            if (emailGenericDailyDto.Date == DateTime.MinValue)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(emailGenericDailyDto.ToEmail))
            {
                return false;
            }


            var message = await _generateMessageDaily.GenerateGenericDailyMessageAsync(emailGenericDailyDto.Asset, emailGenericDailyDto.Date, emailGenericDailyDto.ToEmail);
            if (message == null)
            {
                _logger.LogError("Email message generation failed for symbol: {Symbol}, date: {Date}, toEmail: {ToEmail}", emailGenericDailyDto.Asset, emailGenericDailyDto.Date, emailGenericDailyDto.ToEmail);
                return false;
            }

            bool sendEmail = await _sendGridIntegraion.SendEmailAsync(message);

            if (!sendEmail)
            {
                _logger.LogError("{Classe} Não foi possível enviar para a integracao do SendGrid", nameof(EmailExecutor));
                return false;
            }

            _logger.LogInformation("{Classe} Enviado com sucesso para a integracao do SendGrid", nameof(EmailExecutor));
            return true;

        }
    }
}
