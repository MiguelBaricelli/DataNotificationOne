using DataNotificationOne.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNotificationOne.Application.Interfaces
{
    public interface IEmailExecutor
    {
        Task<bool> ExecuteEmailDailyAsync(InputEmailGenericDailyDto inputEmailGeneric);
    }
}
