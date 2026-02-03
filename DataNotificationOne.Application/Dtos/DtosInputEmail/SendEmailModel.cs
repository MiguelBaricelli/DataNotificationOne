using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNotificationOne.Application.Dtos.DtosInputEmail
{
    public class SendEmailModel
    {

        public required string Asset { get; set; } 
        public required DateTime Date { get; set; }
        public string ToEmail { get; set; } = string.Empty;
        public string FromEmail { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;

    }
}
