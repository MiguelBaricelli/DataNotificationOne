using DataNotificationOne.Application.Dtos;
using DataNotificationOne.Application.Interfaces;
using DataNotificationOne.Domain.Interfaces.Infra;

namespace DataNotificationOne.Application.Services
{

    public class GetFinanceSummaryVarianceService : IGetFinanceSummaryVarianceAsync
    {
        private readonly IAlphaVantageDailyConsumer _client;



        public GetFinanceSummaryVarianceService(IAlphaVantageDailyConsumer client)
        {
            _client = client;
        }

        public async Task<FinanceSummaryDto> GetFinanceSummaryVarianceAsync(string ativo)
        {

            if (string.IsNullOrWhiteSpace(ativo))
            {
                throw new ArgumentNullException("Ativo é obrigatorio");
            }
            var data = await _client.GetTimeSeriesDailyAsync(ativo);

            bool isAlta;
            if (data.Close > data.Open)
            {

                isAlta = true;

            }
            else
            {
                isAlta = false;
            }

            decimal variation =
            Math.Round((data.Close - data.Open) / data.Open * 100, 2);

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
    }
}
