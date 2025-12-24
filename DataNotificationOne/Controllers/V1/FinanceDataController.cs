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

        /// <summary>
        /// Calcula e retorna a variância dos preços de um ativo (open, high, low, close).
        /// Importante: o parâmetro "ativo" é obrigatório.
        /// </summary>
        /// <param name="ativo">Símbolo do ativo (ex.: MSFT, AAPL, IBM)</param>
        /// <returns>Objeto FinanceSummaryDto com variância e status do ativo</returns>
        [HttpGet("GetVariationAsset/{ativo}")]
        [ProducesResponseType(typeof(FinanceSummaryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
         public async Task<ActionResult<FinanceSummaryDto>> GetFinanceSummaryVarianceController(string ativo)
         {
             try
             {
                 if (string.IsNullOrWhiteSpace(ativo))
                 {
                     return BadRequest("Passe o ativo corretamente");
                 }

                 var summary = await _dataFinanceService.GetFinanceSummaryVarianceAsync(ativo);

                 if (summary == null)
                 {
                     return NotFound("Dados não encontrados");
                 }

                 return Ok(summary);
             }
             catch (Exception ex)
             {
                 throw new Exception("Erro ao buscar os dados.", ex);
             }
         }

         /// <summary>
         /// Retorna os dados semanais mais recentes de um ativo.
         /// Importante: o parâmetro "ativo" é obrigatório.
         /// </summary>
         /// <param name="ativo">Símbolo do ativo (ex.: MSFT, AAPL, IBM)</param>
         /// <returns>Lista FinanceDataModel com os dados das últimas 10 semanas</returns>
         [HttpGet("Last10Days/{ativo}")]
         [ProducesResponseType(typeof(IEnumerable<FinanceDataModel>), StatusCodes.Status200OK)]
         [ProducesResponseType(StatusCodes.Status400BadRequest)]
         [ProducesResponseType(StatusCodes.Status404NotFound)]
          public async Task<ActionResult<IEnumerable<FinanceDataModel>>> GetAllWeeklyDataController(string ativo)
          {
              try
              {
                  if (string.IsNullOrWhiteSpace(ativo))
                  {
                      return BadRequest("Passe o ativo corretamente");
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

          /// <summary>
          /// Retorna os dados de uma semana específica para o ativo informado.
          /// Importante: o parâmetro "ativo" é obrigatório e a data deve ser um dia útil.
          /// </summary>
          /// <param name="ativo">Símbolo do ativo (ex.: MSFT, AAPL, IBM)</param>
          /// <param name="date">Data da semana desejada (formato yyyy-MM-dd)</param>
          /// <returns>Objeto FinanceDataModel com os dados da semana solicitada</returns>
          [HttpGet("DataSpecificWeekly/{ativo}/{date}")]
          [ProducesResponseType(typeof(FinanceDataModel), StatusCodes.Status200OK)]
          [ProducesResponseType(StatusCodes.Status400BadRequest)]
          [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FinanceDataModel>> GetAllWeeklyDataController(string ativo, DateTime date)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ativo))
                {
                    return BadRequest("Passe o ativo corretamente");
                }

                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
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
