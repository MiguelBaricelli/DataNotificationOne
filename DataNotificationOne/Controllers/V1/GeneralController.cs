using DataNotificationOne.Application.Interfaces;
using DataNotificationOne.Application.Services;
using DataNotificationOne.Domain.Models;
using DataNotificationOne.Domain.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace DataNotificationOne.Controllers.V1
{
    [ApiController]
    [Route("api/v1")]
    public class GeneralController : ControllerBase
    {

        private readonly GeneralResponseService _generalResponseService;

        public GeneralController(GeneralResponseService generateResponseService)
        {
            _generalResponseService = generateResponseService;

        }

        [HttpGet("GetGeneral/{asset}/{date}/{function}")]
        [ProducesResponseType(typeof(FinanceDataModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(FinanceDataModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(FinanceDataModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FinanceDataModel>> GetGeneral(string asset, DateTime date, FunctionAlphaVantageEnum function)
        {
            if (string.IsNullOrWhiteSpace(asset))
            {
                return BadRequest("O ativo não pode ser nulo ou vazio");
            }
            if (date == default)
            {
                return BadRequest("A data fornecida é inválida");
            }

            if (function == 0){
                return BadRequest("A função fornecida é inválida");
            }

            var response = await _generalResponseService.GeneralResponseServiceAsync(asset, date, function);

            if(response is null)
            {
                return NotFound("Não encontrado nenhum dado");
            }
            

            return Ok(response);
        }
    }
}
