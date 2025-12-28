using DataNotificationOne.Application.Interfaces;
using DataNotificationOne.Domain.Interfaces.Infra;
using DataNotificationOne.Domain.BuildTemplates;
using DataNotificationOne.Domain.Services;

namespace DataNotificationOne.Application.Services
{
    public class GenerateMessageDailyService : IGenerateMessageDailyService
    {
        private readonly IFinanceSummaryVarianceService _financeSummaryVarianceService;
        private readonly BuildTemplates _buildTemplates;

        public GenerateMessageDailyService(IFinanceSummaryVarianceService financeSummaryVarianceService, BuildTemplates buildTemplates)
        {
            _financeSummaryVarianceService = financeSummaryVarianceService;
            _buildTemplates = buildTemplates;
        }

        public async Task<string> GenerateDailyVarianceMessageAsync(string symbol, DateTime date)
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

            var templatePath = Path.Combine(AppContext.BaseDirectory, "Templates", "VarianceTemplateEmail.html");
            var templateHtml = await File.ReadAllTextAsync(templatePath);

            var html =  _buildTemplates.BuildDailyHtmlVariance(
                templateHtml,
                symbol,
                date,
                responseData.Open,
                responseData.High,
                responseData.Low,
                responseData.Close,
                responseData.Variation,
                responseData.IsAlta,
                responseData.MessageIsAlta
            );

            return html;
        }

        public async Task<string> GenerateGenericDailyMessageAsync(string symbol, DateTime date)
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

            if (!data.TryGetValue(dateKey, out var responseData))
            {
                throw new Exception("Dados financeiros não encontrados para a data fornecida.");
            }

            var templatePath = Path.Combine(AppContext.BaseDirectory, "Templates", "DailyTemplateEmailGeneric.html");
            var templateHtml = await File.ReadAllTextAsync(templatePath);

            var html = _buildTemplates.BuildDailyHtmlGeneric(
                templateHtml,
                symbol,
                date,
                responseData.Open,
                responseData.High,
                responseData.Low,
                responseData.Close
            );

            return html;
        }

        public async Task<string> GenerateCustomDailyEmailByClientAsync(string nameClient, string symbol, DateTime date)
        {
            var data = await _financeSummaryVarianceService.GetFinanceSummaryVarianceAsync(symbol, date);
            var keyDate = date.ToString("yyyy-MM-dd");

            if (!data.TryGetValue(keyDate, out var responseData))
                throw new Exception("Dados financeiros não encontrados para a data fornecida.");

            var templatePath = Path.Combine(AppContext.BaseDirectory, "Templates", "DailyTemplateEmailByClient.html");
            var templateHtml = await File.ReadAllTextAsync(templatePath);

            var html = _buildTemplates.BuildDailyHtmlForClient(
                templateHtml,
                nameClient,
                symbol,
                date,
                responseData.Open,
                responseData.High,
                responseData.Low,
                responseData.Close,
                responseData.Variation,
                responseData.IsAlta,
                responseData.MessageIsAlta
            );

            return html;
        }

    }
}
