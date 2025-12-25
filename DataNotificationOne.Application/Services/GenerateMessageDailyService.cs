using DataNotificationOne.Domain.Interfaces.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNotificationOne.Application.Services
{
    public class GenerateMessageDailyService
    {
        private readonly FinanceSummaryVarianceService _financeSummaryVarianceService;

        public GenerateMessageDailyService(IAlphaVantageDailyConsumer alphaVantageDailyConsumer, FinanceSummaryVarianceService financeSummaryVarianceService)
        {
            _financeSummaryVarianceService = financeSummaryVarianceService;
        }

        public async Task<string> GenerateDailyVarianceMessageAsync(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol))
            {
                throw new ArgumentNullException("O símbolo é obrigatório.");
            }
            var data = await _financeSummaryVarianceService.GetFinanceSummaryVarianceAsync(symbol);

            string message = "O ativo {symbol} teve uma variação de {data.Variation}% hoje. ";

            return message;
        }

        public async Task<string> GenerateCustomDailyMessageAsync(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol))
            {
                throw new ArgumentNullException("O símbolo é obrigatório.");
            }

            var data = await _financeSummaryVarianceService.GetFinanceSummaryVarianceAsync(symbol);

            if (data == null)
            {
                throw new Exception("Dados financeiros não encontrados para o símbolo fornecido.");
            }

            var message = $"O ativo {symbol} apresentou as seguintes variações hoje: " +
                $"Abertura: {data.Open}%, Máxima: {data.High}%, " +
                $"Mínima: {data.Low}%, " +
                $"Fechamento: {data.Close}%. " +
                $"A variação total foi de {data.Variation}%." +
                $"Em Alta: {data.IsAlta}";

            return message;
        }

        public async Task<string> GenerateCustomDailyMessageByClientAsync(string nameClient, string symbol, DateTime date)
        {
            if (string.IsNullOrWhiteSpace(symbol))
            {
                throw new ArgumentNullException("O símbolo é obrigatório.");
            }

            if(string.IsNullOrWhiteSpace(date.ToString()))
            {
                throw new ArgumentNullException("A data é obrigatória.");
            }

            var data = await _financeSummaryVarianceService.GetFinanceSummaryVarianceAsync(symbol, date);

            if (data == null)
            {
                throw new Exception("Dados financeiros não encontrados para o símbolo fornecido.");
            }

            var keyDate = date.ToString("yyyy-MM-dd");

            if(!data.TryGetValue(keyDate, out var responseData))
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
