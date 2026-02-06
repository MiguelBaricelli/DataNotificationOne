using DataNotificationOne.Application.Dtos.AuthSecurity;
using DataNotificationOne.Domain.Models.ApiClientSecurity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNotificationOne.Application.Interfaces
{
    public interface IAuthService
    {

        bool ValidateApiKey(string apiKey);

        JwtTokenModel GenerateToken();

    }
}
