using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DataNotificationOne.Controllers.V1.Email.SendNotification
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SendNotificationController
    {
        ILogger<SendNotificationController> _logger;

        public SendNotificationController(ILogger<SendNotificationController> logger)
        {
            _logger = logger;
        }

        [HttpPost("sendEmail")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public Task<IActionResult> Post()
        {
            _logger.LogInformation("Iniciando envio de email");
            return Ok("Notification Service is running.");
        }
    }
}
