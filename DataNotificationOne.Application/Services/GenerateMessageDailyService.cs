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
                <!DOCTYPE html>
                <html lang='pt-BR'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <style>
        body {{
            margin: 10;
            padding: 10;
            background-color: #f3f2f1;
            font-family: Segoe UI, Arial, sans-serif;
            color: #323130;
        }}
        .container {{
            width: 100%;
            padding: 20px 0;
            margin: 10px
        }}
        .card {{
            width: 600px;
            margin: 15px;
            background-color: #ffffff;
            border: 1px solid #edebe9;
        }}
        .header {{
            padding: 20px 24px;
            border-bottom: 1px solid #edebe9;
            font-size: 18px;
            font-weight: 600;
            color: #323130;
        }}
        .content {{
            padding: 24px;
            font-size: 14px;
            line-height: 1.6;
        }}
        .date {{
            font-size: 16px;
            color: #605e5c;
            margin:20px;
        }}
        .symbol {{
            font-weight: 600;
            color: #005a9e;
        }}
        table {{
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
        }}
        td {{
            padding: 8px 0;
            border-bottom: 1px solid #edebe9;
            font-size: 14px;
        }}
        td:last-child {{
            text-align: right;
            font-weight: 600;
        }}
        .variation {{
            background-color: #faf9f8;
            border-left: 4px solid #c8c6c4;
            padding: 12px 16px;
            font-size: 14px;
            color: #323130;
        }}
        .badge-up {{
            background-color: #dff6dd;
            color: #107c10;
            padding: 2px 8px;
            font-size: 12px;
            font-weight: 600;
            border-radius: 2px;
        }}
        .badge-down {{
            background-color: #fde7e9;
            color: #a80000;
            padding: 2px 8px;
            font-size: 12px;
            font-weight: 600;
            border-radius: 2px;
        }}
        .footer {{
            padding: 16px 24px;
            border-top: 1px solid #edebe9;
            font-size: 11px;
            color: #605e5c;
            text-align: center;
        }}
    </style>
                </head>
                <body>
                  <div class='container'>
                    <div class='card'>
                      <div class='date'>No dia {date:yyyy-MM-dd}, o ativo <span class='symbol'>{symbol.ToUpper()}</span> apresentou as seguintes variações:</div>
                      <table class='table'>
                        <tr><td>Abertura</td><td>{responseData.Open}</td></tr>
                        <tr><td>Máxima</td><td>{responseData.High}</td></tr>
                        <tr><td>Mínima</td><td>{responseData.Low}</td></tr>
                        <tr><td>Fechamento</td><td>{responseData.Close}</td></tr>
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
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <style>
                            body {{
                                margin: 10;
                                padding: 10;
                                background-color: #f3f2f1;
                                font-family: Segoe UI, Arial, sans-serif;
                                color: #323130;
                            }}
                            .container {{
                                width: 100%;
                                padding: 20px 0;
                                margin: 10px
                            }}
                            .card {{
                                width: 600px;
                                margin: 15px;
                                background-color: #ffffff;
                                border: 1px solid #edebe9;
                            }}
                            .header {{
                                padding: 20px 24px;
                                border-bottom: 1px solid #edebe9;
                                font-size: 18px;
                                font-weight: 600;
                                color: #323130;
                            }}
                            .content {{
                                padding: 24px;
                                font-size: 14px;
                                line-height: 1.6;
                            }}
                            .date {{
                                font-size: 16px;
                                color: #605e5c;
                                margin:20px;
                            }}
                            .symbol {{
                                font-weight: 600;
                                color: #005a9e;
                            }}
                            table {{
                                width: 100%;
                                border-collapse: collapse;
                                margin-bottom: 20px;
                            }}
                            td {{
                                padding: 8px 0;
                                border-bottom: 1px solid #edebe9;
                                font-size: 14px;
                            }}
                            td:last-child {{
                                text-align: right;
                                font-weight: 600;
                            }}
                            .variation {{
                                background-color: #faf9f8;
                                border-left: 4px solid #c8c6c4;
                                padding: 12px 16px;
                                font-size: 14px;
                                color: #323130;
                            }}
                            .badge-up {{
                                background-color: #dff6dd;
                                color: #107c10;
                                padding: 2px 8px;
                                font-size: 12px;
                                font-weight: 600;
                                border-radius: 2px;
                            }}
                            .badge-down {{
                                background-color: #fde7e9;
                                color: #a80000;
                                padding: 2px 8px;
                                font-size: 12px;
                                font-weight: 600;
                                border-radius: 2px;
                            }}
                            .footer {{
                                padding: 16px 24px;
                                border-top: 1px solid #edebe9;
                                font-size: 11px;
                                color: #605e5c;
                                text-align: center;
                            }}
                        </style>

            </head>
            <body>
              <div class='container'>
                <div class='card'>
                  <div class='greet'>Olá {clientName},</div>
                  <div class='date'>No dia {date:yyyy-MM-dd}, o ativo <span class='symbol'>{asset.ToUpper()}</span> apresentou as seguintes variações:</div>
                  <table class='table'>
                    <tr><td>Abertura</td><td>{responseData.Open}</td></tr>
                    <tr><td>Máxima</td><td>{responseData.High}</td></tr>
                    <tr><td>Mínima</td><td>{responseData.Low}</td></tr>
                    <tr><td>Fechamento</td><td>{responseData.Close}</td></tr>
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
                Subject = $"Resumo diário do ativo {asset.ToUpper()}",
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
            margin: 10;
            padding: 10;
            background-color: #f3f2f1;
            font-family: Segoe UI, Arial, sans-serif;
            color: #323130;
        }}
        .container {{
            width: 100%;
            padding: 20px 0;
            margin: 10px
        }}
        .card {{
            width: 600px;
            margin: 15px;
            background-color: #ffffff;
            border: 1px solid #edebe9;
        }}
        .header {{
            padding: 20px 24px;
            border-bottom: 1px solid #edebe9;
            font-size: 18px;
            font-weight: 600;
            color: #323130;
        }}
        .content {{
            padding: 24px;
            font-size: 14px;
            line-height: 1.6;
        }}
        .date {{
            font-size: 16px;
            color: #605e5c;
            margin:20px;
        }}
        .symbol {{
            font-weight: 600;
            color: #005a9e;
        }}
        table {{
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
        }}
        td {{
            padding: 8px 0;
            border-bottom: 1px solid #edebe9;
            font-size: 14px;
        }}
        td:last-child {{
            text-align: right;
            font-weight: 600;
        }}
        .variation {{
            background-color: #faf9f8;
            border-left: 4px solid #c8c6c4;
            padding: 12px 16px;
            font-size: 14px;
            color: #323130;
        }}
        .badge-up {{
            background-color: #dff6dd;
            color: #107c10;
            padding: 2px 8px;
            font-size: 12px;
            font-weight: 600;
            border-radius: 2px;
        }}
        .badge-down {{
            background-color: #fde7e9;
            color: #a80000;
            padding: 2px 8px;
            font-size: 12px;
            font-weight: 600;
            border-radius: 2px;
        }}
        .footer {{
            padding: 16px 24px;
            border-top: 1px solid #edebe9;
            font-size: 11px;
            color: #605e5c;
            text-align: center;
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
