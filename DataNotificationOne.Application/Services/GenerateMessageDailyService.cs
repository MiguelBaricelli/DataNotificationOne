using DataNotificationOne.Application.Interfaces;
using DataNotificationOne.Domain.Models.Email;

namespace DataNotificationOne.Application.Services
{
    public class GenerateMessageDailyService : IGenerateMessageDailyService
    {
        private readonly IFinanceSummaryVarianceService _financeSummaryVarianceService;

        public GenerateMessageDailyService(
            IFinanceSummaryVarianceService financeSummaryVarianceService)
        {
            _financeSummaryVarianceService = financeSummaryVarianceService;
        }

        public async Task<EmailModel> GenerateGenericDailyMessageAsync(string symbol, DateTime date, string toEmail)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentNullException("O símbolo é obrigatório.");
            if (date == default)
                throw new ArgumentNullException("data nao esta correta");
            if (string.IsNullOrWhiteSpace(toEmail))
                throw new ArgumentNullException("O email do destinatário é obrigatório.");

            var data = await _financeSummaryVarianceService.GetFinanceSummaryVarianceAsync(symbol, date);

            if (data == null)
                throw new Exception("Dados financeiros não encontrados para o símbolo fornecido.");

            var dateKey = date.ToString("yyyy-MM-dd");

            if (!data.TryGetValue(dateKey, out var responseData))
                throw new Exception("Dados financeiros não encontrados para a data fornecida.");

            // Monta o HTML
            var html = $@"
            <html>
              <body style='font-family:Arial,Helvetica,sans-serif;'>
                <h2>Resumo diário do ativo {symbol}</h2>
                <p>Data: {dateKey}</p>
                <ul>
                  <li>Abertura: {responseData.Open}</li>
                  <li>Máxima: {responseData.High}</li>
                  <li>Mínima: {responseData.Low}</li>
                  <li>Fechamento: {responseData.Close}</li>
                  <li><strong>Variação total: {responseData.Variation}%</strong></li>
                </ul>
                <p><strong>Tendência:</strong> {(responseData.IsAlta ? "<span style='color:green;'>Alta</span>" : "<span style='color:red;'>Baixa</span>")} — {responseData.MessageIsAlta}</p>
              </body>
            </html>";

            // Cria o modelo de email
            var emailModel = new EmailModel
            {
                ToEmail = toEmail,
                Subject = $"Resumo diário do ativo {symbol}",
                Asset = symbol,
                Date = date,
                Content = html
            };


            return emailModel;
        }



        public async Task<EmailModel> GenerateCustomDailyEmailByClientAsync(string clientName, string asset, DateTime date, string toEmail)
        {
            var data = await _financeSummaryVarianceService.GetFinanceSummaryVarianceAsync(asset, date);
            var keyDate = date.ToString("yyyy-MM-dd");

            if (!data.TryGetValue(keyDate, out var responseData))
                throw new Exception("Dados financeiros não encontrados para a data fornecida.");


            var html = $@"
            <!DOCTYPE html>
            <html lang='pt-BR'>
            <head> ... (CSS igual ao template) ... </head>
            <body>
              <div class='container'>
                <div class='card'>
                  <div class='greet'>Olá {clientName},</div>
                  <div class='date'>No dia {date:yyyy-MM-dd}, o ativo <span class='symbol'>{asset}</span> apresentou as seguintes variações:</div>
                  <table class='table'>
                    <tr><td>Abertura</td><td>{responseData.Open}</td></tr>
                    <tr><td>Máxima</td><td>{responseData.High}</td></tr>
                    <tr><td>Mínima</td><td>{responseData.Low}</td></tr>
                    <tr><td>Fechamento</td><td>{responseData.Close}</td></tr>
                    <tr><td><strong>Variação total</strong></td><td><strong>{responseData.Variation}%</strong></td></tr>
                  </table>
                  <div class='variation'>
                    <strong>Tendência:</strong>
                    {(responseData.IsAlta ? "<span class='badge-up'>Alta</span>" : "<span class='badge-down'>Baixa</span>")}
                    — {responseData.MessageIsAlta}
                  </div>
                  <div class='footer'>Este email foi gerado automaticamente pela sua API de notificações de mercado.</div>
                </div>
              </div>
            </body>
            </html>";

            var emailModelMessage = new EmailModel
            {
                ToEmail = toEmail,
                Subject = $"Resumo diário do ativo {asset}",
                Asset = asset,
                Date = date,
                Content = html
            };

            return emailModelMessage;
        }
        public async Task<EmailModel> GenerateDailyVarianceMessageAsync(string clientName, string asset, DateTime date, string toEmail)
        {
            var data = await _financeSummaryVarianceService.GetFinanceSummaryVarianceAsync(asset, date);
            var keyDate = date.ToString("yyyy-MM-dd");

            if (!data.TryGetValue(keyDate, out var responseData))
                throw new Exception("Dados financeiros não encontrados para a data fornecida.");


            var html = $@"
            <!DOCTYPE html>
            <html lang='pt-BR'>
            <head> ... (CSS igual ao template) ... </head>
            <body>
              <div class='container'>
                <div class='card'>
                  <div class='greet'>Olá {clientName},</div>
                  <div class='date'>No dia {date:yyyy-MM-dd}, o ativo <span class='symbol'>{asset}</span> apresentou as seguintes variações:</div>
                  <table class='table'>
                    <tr><td>Abertura</td><td>{responseData.Open}</td></tr>
                    <tr><td>Máxima</td><td>{responseData.High}</td></tr>
                    <tr><td>Mínima</td><td>{responseData.Low}</td></tr>
                    <tr><td>Fechamento</td><td>{responseData.Close}</td></tr>
                    <tr><td><strong>Variação total</strong></td><td><strong>{responseData.Variation}%</strong></td></tr>
                  </table>
                  <div class='variation'>
                    <strong>Tendência:</strong>
                    {(responseData.IsAlta ? "<span class='badge-up'>Alta</span>" : "<span class='badge-down'>Baixa</span>")}
                    — {responseData.MessageIsAlta}
                  </div>
                  <div class='footer'>Este email foi gerado automaticamente pela sua API de notificações de mercado.</div>
                </div>
              </div>
            </body>
            </html>";

            var emailModelMessage = new EmailModel
            {
                ToEmail = toEmail,
                Subject = $"Variância diário do ativo {asset}",
                Asset = asset,
                Date = date,
                Content = html
            };

            return emailModelMessage;
        }
    }
}
