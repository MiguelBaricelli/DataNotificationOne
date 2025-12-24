using DataNotificationOne.Application.Dtos;
using DataNotificationOne.Application.Interfaces;
using DataNotificationOne.Domain.Interfaces.Infra;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataNotificationOne.Application
{

    public class FinanceSummaryVarianceService : IFinanceSummaryVarianceService
    {
        private readonly IAlphaVantageDailyConsumer _client;



        public FinanceSummaryVarianceService(IAlphaVantageDailyConsumer client)
        {
            _client = client;
        }

        public async Task<FinanceSummaryDto> GetFinanceSummaryVarianceAsync(string ativo)
        {

            if (string.IsNullOrWhiteSpace(ativo))
            {
                throw new ArgumentNullException("Ativo é obrigatorio");
            }
            var data = await _client.TimeSeriesDailyConsumer(ativo);

            bool isAlta;
            if (data.Close > data.Open)
            {

                isAlta = true;

            }
            else
            {
                isAlta = false;
            }

            var variation = AssetVariation(data.Close, data.Open);

            return new FinanceSummaryDto
            {
                Open = data.Open,
                Close = data.Close,
                High = data.High,
                Low = data.Low,
                Volume = data.Volume,
                IsAlta = isAlta,
                Variation = variation

            };
        }

        public decimal AssetVariation(decimal close, decimal open)
        {

            decimal variation =
             Math.Round(((close - open) / open) * 100, 2);

            return variation;

        }
    }
}
