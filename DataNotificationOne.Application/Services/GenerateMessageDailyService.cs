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
                <h2>Resumo diário do ativo {symbol.ToUpper()}</h2>
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
                Subject = $"Resumo diário do ativo {symbol.ToUpper()}",
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
        public async Task<EmailModel> GenerateDailyVarianceMessageAsync(string asset, DateTime date, string toEmail)
        {
            var data = await _financeSummaryVarianceService.GetFinanceSummaryVarianceAsync(asset, date);
            var keyDate = date.ToString("yyyy-MM-dd");

            if (!data.TryGetValue(keyDate, out var responseData))
                throw new Exception("Dados financeiros não encontrados para a data fornecida.");


            var html = $@"
                <!DOCTYPE html>
                <html lang='pt-BR'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <style>
                        body {{
                            font-family: 'Segoe UI', Arial, sans-serif;
                            background-color: #f4f7f9;
                            margin: 0;
                            padding: 20px;
                            color: #333;
                        }}
                        .container {{
                            max-width: 600px;
                            margin: 0 auto;
                        }}
                        .card {{
                            background: #ffffff;
                            border-radius: 12px;
                            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
                            padding: 30px;
                            border-top: 6px solid #2563eb;
                        }}
                        .greet {{
                            font-size: 22px;
                            font-weight: bold;
                            color: #1e293b;
                            margin-bottom: 8px;
                        }}
                        .date {{
                            font-size: 14px;
                            color: #64748b;
                            margin-bottom: 25px;
                        }}
                        .symbol {{
                            background: #e2e8f0;
                            padding: 2px 8px;
                            border-radius: 4px;
                            font-weight: bold;
                            color: #2563eb;
                        }}
                        .table {{
                            width: 100%;
                            border-collapse: collapse;
                            margin-bottom: 20px;
                        }}
                        .table td {{
                            padding: 12px 0;
                            border-bottom: 1px solid #f1f5f9;
                            font-size: 15px;
                        }}
                        .table td:last-child {{
                            text-align: right;
                            font-weight: 500;
                        }}
                        .variation {{
                            background: #f8fafc;
                            padding: 15px;
                            border-radius: 8px;
                            font-size: 14px;
                            line-height: 1.6;
                            border-left: 4px solid #cbd5e1;
                        }}
                        .badge-up {{
                            background-color: #dcfce7;
                            color: #166534;
                            padding: 3px 10px;
                            border-radius: 12px;
                            font-weight: bold;
                            font-size: 12px;
                        }}
                        .badge-down {{
                            background-color: #fee2e2;
                            color: #991b1b;
                            padding: 3px 10px;
                            border-radius: 12px;
                            font-weight: bold;
                            font-size: 12px;
                        }}
                        .footer {{
                            margin-top: 30px;
                            font-size: 11px;
                            color: #94a3b8;
                            text-align: center;
                            border-top: 1px solid #f1f5f9;
                            padding-top: 15px;
                        }}
                    </style>
                </head>
                <body>
                  <div class='container'>
                    <div class='card'>
                      <div class='date'>No dia {date:yyyy-MM-dd}, o ativo <span class='symbol'>{asset.ToUpper()}</span> apresentou as seguintes variações:</div>
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
                Subject = $"Variação diário do ativo {asset}",
                Asset = asset,
                Date = date,
                Content = html
            };

            return emailModelMessage;
        }
    }
}
