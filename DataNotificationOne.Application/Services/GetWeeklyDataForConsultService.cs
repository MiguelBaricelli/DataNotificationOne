using DataNotificationOne.Application.Interfaces;
using DataNotificationOne.Domain.Interfaces.Infra;
using DataNotificationOne.Domain.Models;
using DataNotificationOne.Domain.Services;
using System.Globalization;

namespace DataNotificationOne.Application.Services
{
    public class GetWeeklyDataForConsultService : IGetWeeklyDataForConsultService
    {

        private readonly HttpClient _httpClient;
        private readonly IAlphaVantageWeeklyConsumer _consumer;

        public GetWeeklyDataForConsultService(HttpClient httpClient, IAlphaVantageWeeklyConsumer consumer)
        {
            _httpClient = httpClient;
            _consumer = consumer;
        }

        public async Task<FinanceDataModel> GetWeeklyDataAsync(string symbol)
        {

            if (string.IsNullOrEmpty(symbol))
            {
                throw new ArgumentNullException("Precisa conter o simbolo");
            }
            var request = await _consumer.GetWeeklyDataAsync(symbol);

            if (request == null)
            {
                throw new ArgumentNullException("Objeto não encontrado");
            }

            return new FinanceDataModel
            {
                WeekDate = request.WeekDate,
                Open = request.Open,
                Close = request.Close,
                Low = request.Low,
                High = request.High,
                Volume = request.Volume,

            };
        }

        public async Task<FinanceDataModel> GetDataByWeekly(string symbol, DateTime date)
        {
            var request = await _consumer.GetWeeklyDataAsync(symbol);

            if (request == null)
            {
                throw new Exception("Não foi possível acessar os dados");
            }

            string dateKey = date.ToString("yyyy-MM-dd");

            // Monta o DTO com dados vindos da infra
            var dailyDto = new AlphaVantageDailyDto
            {
                Open = request.Open.ToString(CultureInfo.InvariantCulture),
                High = request.High.ToString(CultureInfo.InvariantCulture),
                Low = request.Low.ToString(CultureInfo.InvariantCulture),
                Close = request.Close.ToString(CultureInfo.InvariantCulture),
                Volume = request.Volume.ToString()
            };

            var lista = new Dictionary<string, AlphaVantageDailyDto>();

            foreach (var n in lista) {
                lista.Add(dateKey, dailyDto);
            }


            return new FinanceDataModel
            {
                WeekDate = DateTime.Parse(dateKey),
                Open = request.Open,
                Close = request.Close,
                Low = request.Low,
                High = request.High,
                Volume = request.Volume > 0 ? request.Volume : 0,

            };
        }

        public async Task<List<FinanceDataModel>> GetAllDataByWeekly(string symbol)
        {
            var request = await _consumer.GetWeeklyDataAsync(symbol);

            if (request == null)
                throw new Exception("Não foi possível acessar os dados");

            // 🔹 Últimas 10 semanas
            var result = request
                .OrderByDescending(x => x.WeekDate) // mais recente primeiro
                .Take(10)
                .Select(x => new FinanceDataModel
                {
                    WeekDate = x.WeekDate,
                    Open = x.Open,
                    High = x.High,
                    Low = x.Low,
                    Close = x.Close,
                    Volume = x.Volume
                })
                .ToList();

            return result;
        }

    }
}
