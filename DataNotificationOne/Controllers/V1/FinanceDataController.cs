using DataNotificationOne.Application;
using DataNotificationOne.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DataNotificationOne.Controllers.V1
{

    [ApiController]
    [Route("api/[controller]")]
    public class FinanceDataController : ControllerBase
    {

        private readonly GetFinanceSummaryVarianceService _dataFinanceService;

        public FinanceDataController(GetFinanceSummaryVarianceService dataFinanceService)
        {
            _dataFinanceService = dataFinanceService;
        }

        [HttpGet("PegarVarianciaDeAtivo/{ativo}")]
        public async Task<ActionResult<FinanceSummaryDto>> GetFinanceSummaryVarianceController(string ativo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ativo))
                {
                    Console.WriteLine("Obrigatório passar o ativo");
                    return BadRequest("Passe o ativo corretamente");
                }

                var summary = await _dataFinanceService.GetFinanceSummaryVarianceAsync(ativo);
               
                if(summary == null)
                {
                    Console.WriteLine("Erro, dados não foram encontrados");
                    return NotFound("Dados não encontrados");
                }

                return Ok(summary);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar os dados. {ex}", ex);
            }
        }
    }
}
