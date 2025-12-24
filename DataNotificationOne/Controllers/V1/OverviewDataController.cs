using DataNotificationOne.Application.Services;
using DataNotificationOne.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace DataNotificationOne.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]")]
    public class OverviewDataController : ControllerBase
    {
        private readonly DataOverviewService _dataOverviewService;

        public OverviewDataController(DataOverviewService dataOverviewService)
        {
            _dataOverviewService = dataOverviewService;
        }

        /// <summary>
        /// 
        [HttpGet("GetOverviewData/{ativo}")]
        [ProducesResponseType(typeof(OverviewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(OverviewModel),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(OverviewModel),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OverviewModel>> GetAllDataOverviewController(string ativo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ativo))
                {
                    return BadRequest("Passe o ativo corretamente");
                }
                var overviewData = await _dataOverviewService.GetAllDataOverviewBySymbolServiceAsync(ativo);

                if (overviewData == null)
                {
                    return NotFound("Dados não encontrados");
                }
                return Ok(overviewData);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar os dados.", ex);
            }
        }

        /// <summary>
        /// 
        [HttpGet("GetSummaryCompany/{ativo}")]
        [ProducesResponseType(typeof(OverviewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(OverviewModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(OverviewModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OverviewModel>> GetSummaryCompanyOverviewController(string ativo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ativo))
                {
                    return BadRequest("Passe o ativo corretamente");
                }
                var overviewData = await _dataOverviewService.GetCompanyOverviewSummaryServiceAsync(ativo);

                if (overviewData == null)
                {
                    return NotFound("Dados não encontrados");
                }
                return Ok(overviewData);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar os dados.", ex);
            }
        }
    }
}
