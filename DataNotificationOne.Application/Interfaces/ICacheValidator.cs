using DataNotificationOne.Domain.Models.BraApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNotificationOne.Application.Interfaces
{
     public interface ICacheValidator
    {
        Task<T> CacheValidatorAsync<T>(string symbol, Func<Task<T>> fetchData) where T : class;
    }
}
