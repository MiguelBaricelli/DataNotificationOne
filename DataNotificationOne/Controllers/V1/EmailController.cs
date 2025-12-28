using DataNotificationOne.Application.Interfaces;
using DataNotificationOne.Domain.Interfaces.Infra;
using DataNotificationOne.Domain.Models.Email;
using DataNotificationOne.Infrastructure.ExternalApis.Email;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DataNotificationOne.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly ISendGridIntegration _sendGridIntegration;
        private readonly IGenerateMessageDailyService _generateMessageDaily;
        private readonly ILogger<EmailController> _logger;
        public EmailController( 
            ILogger<EmailController> iLogger, 
            IGenerateMessageDailyService generateMessageDaily,
            ISendGridIntegration sendGridIntegration)
        {
           _logger = iLogger;
            _generateMessageDaily = generateMessageDaily;
            _sendGridIntegration = sendGridIntegration;
        }

        [HttpPost("sendGenericEmail")]

        public async Task<ActionResult<string>> SendGenericDailyEmail(EmailModel emailModel)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(emailModel.asset)) 
                    throw new ArgumentNullException(nameof(emailModel.asset));
                if (emailModel.date == DateTime.MinValue)
                    throw new ArgumentNullException(nameof(emailModel.date));
                if (string.IsNullOrWhiteSpace(emailModel.toEmail))
                    throw new ArgumentNullException(nameof(emailModel.toEmail));


                var generatingMessage = await _generateMessageDaily.GenerateGenericDailyMessageAsync(emailModel.asset, emailModel.date).ConfigureAwait(false);

                if (string.IsNullOrWhiteSpace(generatingMessage))
                {
                    _logger.LogError("Dados não foram encontrados para asset {} e data {}", emailModel.asset, emailModel.date);
                    return NotFound("Dados não foram enviados");
                }
                 await _sendGridIntegration.SendEmailAsync(emailModel).ConfigureAwait(false);

                  return Ok(generatingMessage);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
