using DataNotificationOne.Application.Dtos;
using DataNotificationOne.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DataNotificationOne.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IEmailExecutor _emailExecutor;

        public EmailController(
            ILogger<EmailController> iLogger,
            IEmailExecutor emailExecutor)

        {
            _emailExecutor = emailExecutor;
            _logger = iLogger;
        }

        [HttpPost("sendGenericEmail")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<string>> SendGenericDailyEmail([FromBody] InputEmailGenericDailyDto inputEmailDto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(inputEmailDto.Asset) ||
                    inputEmailDto.Date == DateTime.MinValue ||
                    string.IsNullOrWhiteSpace(inputEmailDto.ToEmail))
                {
                    _logger.LogError("{Class} Parâmetros inválidos para o envio do email genérico diario", nameof(SendGenericDailyEmail));
                    return BadRequest("Necessário informar o ativo corretamente");
                }

                var sendEmail = await _emailExecutor.ExecuteEmailDailyAsync(inputEmailDto);

                if (!sendEmail)
                {
                    _logger.LogError("Não foi possivel executar o envio do email genérico diario");
                    return NotFound(sendEmail);
                }
                return Ok(sendEmail);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
