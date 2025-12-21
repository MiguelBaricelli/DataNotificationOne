using DataNotificationOne.Application.Dtos;
using DataNotificationOne.Domain.Interfaces.Infra;
using DataNotificationOne.Domain.Models;
using DataNotificationOne.Domain.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNotificationOne.Application.Services
{
    public class GetWeeklyDataForConsultService
    {

        private HttpClient _httpClient;
        private IAlphaVantageWeeklyConsumer _consumer;

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

            //Preciso ajustar os contratos para que pare de dar erro na hora de filtrar a data do cliente.
            //Responsabilidade precisa ficar no service e infra só me passar os dados crus.

            //Ajustar essa classe e todos o restante  do método

            // Intervalo da semana do cliente (segunda → domingo)
            var startOfWeek = date.AddDays(
                -(int)date.DayOfWeek + (int)DayOfWeek.Monday);

            return new FinanceDataModel
            {
                WeekDate = date,
                Open = request.Open,
                Close = request.Close,
                Low = request.Low,
                High = request.High,
                Volume = request.Volume > 0 ? request.Volume : 0,

            };
        }



    }
}
