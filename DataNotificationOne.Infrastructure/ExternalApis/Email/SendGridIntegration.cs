using DataNotificationOne.Application.Interfaces;
using DataNotificationOne.Domain.Interfaces.Infra;
using DataNotificationOne.Domain.Models.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace DataNotificationOne.Infrastructure.ExternalApis.Email
{
    public class SendGridIntegration : ISendGridIntegration
    {
        private readonly IConfiguration _config;
        private readonly string _apiKey;
        private readonly ILogger<SendGridIntegration> _logger;
        private readonly IGenerateMessageDailyService _generateMessageDaily;


        public SendGridIntegration(IConfiguration config, ILogger<SendGridIntegration> logger, IGenerateMessageDailyService generateMessageDaily)
        {
            _generateMessageDaily = generateMessageDaily;
            _config = config;
            _logger = logger;
            _apiKey = _config["ApiKeys:SendGrid"]
                ?? throw new Exception("API Key SendGrid não configurada");
        }

        public async Task SendEmailAsync(EmailModel emailModel)
        {
            if (string.IsNullOrEmpty(emailModel.toEmail))
                throw new ArgumentException("O email do destinatário não pode ser nulo ou vazio.", nameof(emailModel.toEmail));
            if (string.IsNullOrEmpty(emailModel.subject))
                throw new ArgumentException("O assunto do email não pode ser nulo ou vazio.", nameof(emailModel.subject));

            var client = new SendGridClient(_apiKey);
            _logger.LogInformation("SendGridClient criado.");

            var from = new EmailAddress("datamarketnotification@gmail.com", "Data Market Notification");
            var to = new EmailAddress(emailModel.toEmail);

            var subject = emailModel.subject;
            //Estudar como coloca um HML e envia um hml
            var plainTextContent = "Resumo diário do ativo {asset} em {date:yyyy-MM-dd}";
            var htmlContent = await _generateMessageDaily.GenerateGenericDailyMessageAsync(emailModel.asset, emailModel.date);

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            _logger.LogInformation("Enviando mensagem para SendGrid para {ToEmail} com assunto {subject}...", emailModel.toEmail, emailModel.subject);
            var response = await client.SendEmailAsync(msg);

            _logger.LogInformation("SendGrid retornou status {StatusCode}", response.StatusCode);
        }
    }
}