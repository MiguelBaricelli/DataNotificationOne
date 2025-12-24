using DataNotificationOne.Domain.Interfaces.Infra;
using DataNotificationOne.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNotificationOne.Application.Services
{
    public class DataOverviewService
    {

        private readonly IAlphaVantageOverviewConsumer _alphaVantageOverviewConsumer;


        public DataOverviewService(IAlphaVantageOverviewConsumer alphaVantageOverviewConsumer )
        {
            _alphaVantageOverviewConsumer = alphaVantageOverviewConsumer;
        }

        public async Task<OverviewModel> GetAllDataOverviewService(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol) )
            {
                throw new ArgumentNullException("Ativo obrigatorio");
            }


            var data = await _alphaVantageOverviewConsumer.GetCompanyOverviewAsync(symbol);

            //Futuramente guardar esses dados em um db de cache para nao ficar consultando a api toda hora

            if (data == null)
            {
                throw new Exception("Nenhum dado encontrado para o ativo informado.");
            }

            if(data.Symbol == null)
            {
                throw new Exception("Ativo invalido.");
            }

            return data;
        }
    }
}
