using DataNotificationOne.Application.Interfaces;
using DataNotificationOne.Domain.Interfaces.Infra;

namespace DataNotificationOne.Application.Services
{
    public class GenerateMessageDailyService : IGenerateMessageDailyService
    {
        private readonly FinanceSummaryVarianceService _financeSummaryVarianceService;

        public GenerateMessageDailyService(IAlphaVantageDailyConsumer alphaVantageDailyConsumer, FinanceSummaryVarianceService financeSummaryVarianceService)
        {
            _financeSummaryVarianceService = financeSummaryVarianceService;
        }

        public async Task<string> GenerateDailyVarianceMessageAsync(string symbol, DateTime date)
        {
            if (string.IsNullOrWhiteSpace(symbol))
            {
                throw new ArgumentNullException("O símbolo é obrigatório.");
            }
            var data = await _financeSummaryVarianceService.GetFinanceSummaryVarianceAsync(symbol, date);

            string message = "O ativo {symbol} teve uma variação de {data.Variation}% hoje. ";

            return message;
        }

        public async Task<string> GenerateCustomDailyMessageAsync(string symbol, DateTime date)
        {
            if (string.IsNullOrWhiteSpace(symbol))
            {
                throw new ArgumentNullException("O símbolo é obrigatório.");
            }

            var data = await _financeSummaryVarianceService.GetFinanceSummaryVarianceAsync(symbol, date);

            if (data == null)
            {
                throw new Exception("Dados financeiros não encontrados para o símbolo fornecido.");
            }

            var dateKey = date.ToString("yyyy-MM-dd");

            if (data.TryGetValue(dateKey, out var responseData))
            {
                throw new Exception("Dados financeiros não encontrados para a data fornecida.");
            }

            var message = $"O ativo {symbol} apresentou as seguintes variações hoje: " +
                $"Abertura: {responseData.Open}%" +
                $"Máxima: {responseData.High}%, " +
                $"Mínima: {responseData.Low}%, " +
                $"Fechamento: {responseData.Close}%. " +
                $"A variação total foi de {responseData.Variation}%." +
                $"Em Alta: {responseData.IsAlta}" +
                $"Tendencia: {responseData.MessageIsAlta}";


            return message;
        }

        public async Task<string> GenerateCustomDailyMessageByClientAsync(string nameClient, string symbol, DateTime date)
        {
            if (string.IsNullOrWhiteSpace(symbol))
            {
                throw new ArgumentNullException("O símbolo é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(date.ToString()))
            {
                throw new ArgumentNullException("A data é obrigatória.");
            }

            var data = await _financeSummaryVarianceService.GetFinanceSummaryVarianceAsync(symbol, date);

            if (data == null)
            {
                throw new Exception("Dados financeiros não encontrados para o símbolo fornecido.");
            }

            var keyDate = date.ToString("yyyy-MM-dd");

            if (!data.TryGetValue(keyDate, out var responseData))
            {
                throw new Exception("Dados financeiros não encontrados para a data fornecida.");
            }


            var message = $"Olá {nameClient}, \n" +
                $"No dia {keyDate}, " +
                $"O ativo {symbol} apresentou as seguintes variações hoje: \n" +
                $"Abertura: {responseData.Open}%, \n" +
                $"Máxima: {responseData.High}%, \n" +
                $"Mínima: {responseData.Low}%, \n" +
                $"Fechamento: {responseData.Close}%. \n" +
                $"A variação total foi de {responseData.Variation}%.\n" +
                $"Em Alta: {responseData.IsAlta}" +
                $"{responseData.MessageIsAlta}";

            return message;
        }
    }
}
