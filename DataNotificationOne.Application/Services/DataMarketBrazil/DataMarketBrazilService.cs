using DataNotificationOne.Application.Interfaces;
using DataNotificationOne.Domain.Interfaces.Infra.Repository;
using DataNotificationOne.Domain.Models.BraApi;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataNotificationOne.Application.Services.DataMarketBrazil
{
    public class DataMarketBrazilService : IDataMarketBrazilService
    {
        public readonly IBrApiRepository _brApiRepository;
        public DataMarketBrazilService(IBrApiRepository brApiRepository)
        {
            _brApiRepository = brApiRepository;
        }

        public async Task<BrApiRequest> GetAllBrApiDataAsync(string symbol)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                return new BrApiRequest
                {
                    BraApiResults = new List<BrApiModel>(),
                    RequestedAt = DateTime.UtcNow
                };
            }

            return await _brApiRepository.GetBrApiDataAsync(symbol);
        }

        //MINHA LÓGICA COM USO DE FOR ITERANDO PELA POSIÇÃO DO ARRAY (PEGAR UM OU MAIS DE UM ATIVO E RETORNAR DADOS APENAS DO CONTRATO)
        public async Task<List<BrApiRegularModel>> GetRegularDataAsset(string symbol)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                return new List<BrApiRegularModel>();

            }

            var response = await _brApiRepository.GetBrApiDataAsync(symbol);

            if (response == null || response.BraApiResults.Count <= 0)
            {
                return new List<BrApiRegularModel>();
            }

            var listMessage = new List<BrApiRegularModel>();

            for (var i = 0; i < response.BraApiResults.Count; i++)
            {
                var responseMessage = new BrApiRegularModel
                {
                    symbol = response.BraApiResults[i].symbol,
                    shortName = response.BraApiResults[i].shortName,
                    longName = response.BraApiResults[i].longName,
                    currency = response.BraApiResults[i].currency,
                    regularMarketPrice = response.BraApiResults[i].regularMarketPrice,
                    regularMarketDayHigh = response.BraApiResults[i].regularMarketDayHigh,
                    regularMarketDayLow = response.BraApiResults[i].regularMarketDayLow,
                    regularMarketDayRange = response.BraApiResults[i].regularMarketDayRange,
                    regularMarketChange = response.BraApiResults[i].regularMarketChange,
                    regularMarketChangePercent = response.BraApiResults[i].regularMarketChangePercent,
                    regularMarketTime = response.BraApiResults[i].regularMarketTime,
                    marketCap = response.BraApiResults[i].marketCap,
                    regularMarketVolume = response.BraApiResults[i].regularMarketVolume,
                    regularMarketPreviousClose = response.BraApiResults[i].regularMarketPreviousClose,
                    regularMarketOpen = response.BraApiResults[i].regularMarketOpen,
                    fiftyTwoWeekRange = response.BraApiResults[i].fiftyTwoWeekRange
                };

                listMessage.Add(responseMessage);
            }
            return listMessage.ToList();
        }

        //IA, UTILIZA LINQ .SELECT PARA CONSULTA DE LISTA.
        public async Task<List<BrApiRegularModel>> GetRegularDataAssetTEST(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                return new List<BrApiRegularModel>();

            var response = await _brApiRepository.GetBrApiDataAsync(symbol);

            if (response?.BraApiResults == null || !response.BraApiResults.Any())
                return new List<BrApiRegularModel>();

            var resultList = response.BraApiResults
                .Select(result => new BrApiRegularModel
                {
                    symbol = result.symbol,
                    shortName = result.shortName,
                    longName = result.longName,
                    currency = result.currency,
                    regularMarketPrice = result.regularMarketPrice,
                    regularMarketDayHigh = result.regularMarketDayHigh,
                    regularMarketDayLow = result.regularMarketDayLow,
                    regularMarketDayRange = result.regularMarketDayRange,
                    regularMarketChange = result.regularMarketChange,
                    regularMarketChangePercent = result.regularMarketChangePercent,
                    regularMarketTime = result.regularMarketTime,
                    marketCap = result.marketCap,
                    regularMarketVolume = result.regularMarketVolume,
                    regularMarketPreviousClose = result.regularMarketPreviousClose,
                    regularMarketOpen = result.regularMarketOpen,
                    fiftyTwoWeekRange = result.fiftyTwoWeekRange
                })
                .ToList();

            return resultList;
        }
    }
}
    