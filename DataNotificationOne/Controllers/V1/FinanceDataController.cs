using DataNotificationOne.Application;
using DataNotificationOne.Application.Dtos;
using DataNotificationOne.Application.Services;
using DataNotificationOne.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace DataNotificationOne.Controllers.V1
{

    [ApiController]
    [Route("api/[controller]")]
    public class FinanceDataController : ControllerBase
    {

        private readonly GetFinanceSummaryVarianceService _dataFinanceService;
        private readonly GetWeeklyDataForConsultService _getWeeklyDataForConsultService;

        public FinanceDataController(GetFinanceSummaryVarianceService dataFinanceService, GetWeeklyDataForConsultService getWeeklyDataForConsultService)
        {
            _dataFinanceService = dataFinanceService;
            _getWeeklyDataForConsultService = getWeeklyDataForConsultService;
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

                if (summary == null)
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

        // Pegar os dados semanais dos últimos 10 dias úteis
        [HttpGet("Last10Days/{ativo}")]
        public async Task<ActionResult<FinanceDataModel>> GetAllWeeklyDataController(string ativo)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(ativo))
                {
                    return BadRequest("Ativo");
                }

                var data = await _getWeeklyDataForConsultService.GetLastTenWeeklys(ativo);

                if (data == null)
                {
                    return NotFound("Nenhum dado foi retornado do serviço");
                }

                return Ok(data);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro inesperado", ex);
            }
        }

        [HttpGet("DataSpecificWeekly/{ativo}/{data}")]
        public async Task<ActionResult<FinanceDataModel>> GetAllWeeklyDataController(string ativo, DateTime date)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(ativo))
                {
                    return BadRequest("Ativo");
                }

                if (date.DayOfWeek == DayOfWeek.Saturday ||
                    date.DayOfWeek == DayOfWeek.Sunday)
                {
                    return BadRequest("Data deve ser um dia útil.");
                }

                var data = await _getWeeklyDataForConsultService.GetDataByWeekly(ativo, date);

                if (data == null)
                {
                    return NotFound("Nenhum dado foi retornado do serviço");
                }

                return Ok(data);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro inesperado", ex);
            }
        }
    }
}
