using System.Globalization;
using System.Text.Json;
using DataNotificationOne.Domain.Services;
using DataNotificationOne.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
namespace DataNotificationOne.Infrastructure.ExternalApis
{
    public class AlphaVantageClient : IAlphaVantageClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public AlphaVantageClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["ApiKeys:KeyApiFinance"]
                ?? throw new Exception("API Key Alpha Vantage não configurada");
        }

        public async Task<FinanceDataModel> GetFinanceDataAsync(string symbol)
        {
            var url =
                $"https://www.alphavantage.co/query" +
                $"?function=TIME_SERIES_DAILY" +
                $"&symbol={symbol}" +
                $"&apikey={_apiKey}";

            var response = await _httpClient.GetAsync(url);

            if(response == null)
            {
                Console.WriteLine("Dados estão vindo nulos da api");
                throw new Exception("Dados estão vindo nulo da api");
            }

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            if (json.Contains("Error Message"))
                throw new Exception("Ativo inválido ou não encontrado");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var data =
                JsonSerializer.Deserialize<AlphaVantageResponseDto>(json, options)
                ?? throw new Exception("Resposta inválida da Alpha Vantage");

            if(data == null)
            {
                Console.WriteLine("Dados do json vierao nulos");
                throw new Exception("Dados do json vierao nulos");
            }

            // pega o dia mais recente
            var ultimoDia = data.TimeSeries
                .OrderByDescending(x => x.Key)
                .First().Value;

            if(ultimoDia.Volume == null)
            {
                return new FinanceDataModel
                {
                    Open = decimal.Parse(ultimoDia.Open, CultureInfo.InvariantCulture),
                    High = decimal.Parse(ultimoDia.High, CultureInfo.InvariantCulture),
                    Low = decimal.Parse(ultimoDia.Low, CultureInfo.InvariantCulture),
                    Close = decimal.Parse(ultimoDia.Close, CultureInfo.InvariantCulture),
                };
            }

            return new FinanceDataModel
            {
                Open = decimal.Parse(ultimoDia.Open, CultureInfo.InvariantCulture),
                High = decimal.Parse(ultimoDia.High, CultureInfo.InvariantCulture),
                Low = decimal.Parse(ultimoDia.Low, CultureInfo.InvariantCulture),
                Close = decimal.Parse(ultimoDia.Close, CultureInfo.InvariantCulture),
                Volume = long.Parse(ultimoDia.Volume),               
            };
        }
    }
}
