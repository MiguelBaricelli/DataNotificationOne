using DataNotificationOne.Domain.Interfaces.Infra;
using DataNotificationOne.Domain.Models;
using DataNotificationOne.Domain.Services;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Text.Json;

namespace DataNotificationOne.Infrastructure.ExternalApis
{
    public class AlphaVantageWeeklyConsumer : IAlphaVantageWeeklyConsumer
    {

        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public AlphaVantageWeeklyConsumer(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["ApiKeys:KeyApiFinance"]
               ?? throw new Exception("API Key Alpha Vantage não configurada");
        }

        public async Task<FinanceDataModel> GetWeeklyDataAsync(string symbol)
        {
            var url =
                $"https://www.alphavantage.co/query" +
                $"?function=TIME_SERIES_WEEKLY" +
                $"&symbol={symbol}" +
                $"&apikey={_apiKey}";

            var request = await _httpClient.GetAsync(url);

            if (request.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception("Api não foi encontrada");
            }

            if (request.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("Você não está autorizado para utilizar esse serviço");
            }

            if (request.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new Exception("Todos os campos precisam estar corretos");
            }
            if (request.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                throw new Exception("Você não é autorizado para utilizar esse serviço");
            }

            request.EnsureSuccessStatusCode();

            var json = await request.Content.ReadAsStringAsync();

            if (json.Contains("Error Message"))
                throw new Exception("Ativo inválido ou não encontrado");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var data = JsonSerializer.Deserialize<AlphaVantageResponseDto>(json, options)
                ?? throw new Exception("Resposta inválida da Alpha Vantage");

            if (data == null)
            {
                Console.WriteLine("Dados do json vierao nulos");
                throw new Exception("Dados do json vierao nulos");
            }

            //Semana mais recente
            var recentWeekly = data.TimeSeries
             .OrderByDescending(x => x.Key)
             .First();

            return new FinanceDataModel
            {
                WeekDate = DateTime.Parse(recentWeekly.Key),
                Open = decimal.Parse(recentWeekly.Value.Open, CultureInfo.InvariantCulture),
                High = decimal.Parse(recentWeekly.Value.High, CultureInfo.InvariantCulture),
                Low = decimal.Parse(recentWeekly.Value.Low, CultureInfo.InvariantCulture),
                Close = decimal.Parse(recentWeekly.Value.Close, CultureInfo.InvariantCulture),
                Volume = long.Parse(recentWeekly.Value.Volume, CultureInfo.InvariantCulture)
            };
        }

    }
}
