using DataNotificationOne.Application.Dtos;
using DataNotificationOne.Application.Interfaces;
using DataNotificationOne.Domain.Interfaces.Infra;
using DataNotificationOne.Domain.Models;

namespace DataNotificationOne.Application.Services
{
    public class DataOverviewService : IDataOverviewService
    {

        private readonly IAlphaVantageOverviewConsumer _alphaVantageOverviewConsumer;


        public DataOverviewService(IAlphaVantageOverviewConsumer alphaVantageOverviewConsumer)
        {
            _alphaVantageOverviewConsumer = alphaVantageOverviewConsumer;
        }

        public async Task<OverviewModel> GetAllDataOverviewBySymbolServiceAsync(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol))
            {
                throw new ArgumentNullException("Ativo obrigatorio");
            }


            var data = await _alphaVantageOverviewConsumer.OverviewConsumer(symbol);

            //Futuramente guardar esses dados em um db de cache para nao ficar consultando a api toda hora

            if (data == null)
            {
                throw new Exception("Nenhum dado encontrado para o ativo informado.");
            }

            if (data.Symbol == null)
            {
                throw new Exception("Ativo invalido.");
            }

            return data;
        }

        public async Task<SummaryCompanyOverviewDto> GetCompanyOverviewSummaryServiceAsync(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol))
            {
                throw new ArgumentNullException("Ativo obrigatorio");
            }

            var data = await _alphaVantageOverviewConsumer.OverviewConsumer(symbol);

            if (data == null)
            {
                throw new Exception("Nenhum dado encontrado para o ativo informado.");
            }

            var response = new SummaryCompanyOverviewDto
            {
                Symbol = data.Symbol,
                AssetType = data.AssetType,
                Name = data.Name,
                Description = data.Description,
                CIK = data.CIK,
                Currency = data.Currency,
                Country = data.Country,
                Sector = data.Sector,
                Industry = data.Industry,
                Address = data.Address,
                OfficialSite = data.OfficialSite,
                MarketCapitalization = data.MarketCapitalization,
            };

            if (response == null)
            {
                throw new Exception("Dados não encontrados.");
            }

            return response;
        }
    }
}
