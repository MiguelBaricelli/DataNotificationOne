using DataNotificationOne.Application.Dtos;
using DataNotificationOne.Application.Interfaces;
using DataNotificationOne.Domain.Interfaces.Infra;
using DataNotificationOne.Domain.Models;
using DataNotificationOne.Domain.Interfaces.Infra.Repository;
using System.Text.Json;
namespace DataNotificationOne.Application.Services.Overview
{
    public class DataOverviewService : IDataOverviewService
    {

        private readonly IAlphaVantageOverviewConsumer _alphaVantageOverviewConsumer;
        private readonly ICacheRepository _cacheRepository;


        public DataOverviewService(IAlphaVantageOverviewConsumer alphaVantageOverviewConsumer, ICacheRepository cacheRepository)
        {
            _alphaVantageOverviewConsumer = alphaVantageOverviewConsumer;
            _cacheRepository = cacheRepository;
        }

        public async Task<OverviewModel> GetAllDataOverviewBySymbolServiceAsync(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol))
            {
                throw new ArgumentNullException("Ativo obrigatorio");
            }

            var IsCache = await _cacheRepository.GetAsync(symbol).ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(IsCache))
            {
                return JsonSerializer.Deserialize<OverviewModel>(IsCache);
            }
                    
             var data = await _alphaVantageOverviewConsumer.OverviewConsumer(symbol);

            if (data == null)
            {
                throw new Exception("Nenhum dado encontrado para o ativo informado.");
            }

            var json = JsonSerializer.Serialize(data);

            await _cacheRepository.SetAsync(symbol, json, TimeSpan.FromSeconds(120)); 

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
