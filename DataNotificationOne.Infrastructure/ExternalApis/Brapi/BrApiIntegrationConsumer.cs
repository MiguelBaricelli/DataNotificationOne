using DataNotificationOne.Domain.Models.BraApi;
using DataNotificationOne.Domain.Interfaces.Infra;
using System.Text.Json;



namespace DataNotificationOne.Infrastructure.ExternalApis.Brapi
{
    
    public class BrApiIntegrationConsumer : IBrApiIntegrationConsumer
    {
        private readonly HttpClient _httpClient;

        public BrApiIntegrationConsumer(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BrApiRequest> GetBrapiDataAsync(string symbol)
        {
            var url = $"https://brapi.dev/api/quote/{symbol}";
                      

                var response = await _httpClient.GetAsync(url);

                if (response == null)
                {
                    Console.WriteLine("Dados estão vindo nulos da api");
                    throw new Exception("Dados estão vindo nulo da api");
                }

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                if (json.Contains("Error Message"))
                    throw new Exception("Ativo inválido ou não encontrado");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var data =
                    JsonSerializer.Deserialize<BrApiRequest>(json, options)
                    ?? throw new Exception("Resposta inválida da Brapi");

                if (data == null)
                {
                    Console.WriteLine("Dados do json vieram nulos ou vazios");
                    throw new Exception("Dados do json vieram nulos ou vazios");
                }

            
            return data;
            }
        }
    }

