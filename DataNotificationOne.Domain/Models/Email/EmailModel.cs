using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNotificationOne.Domain.Models.Email
{
    public class EmailModel
    {
        public string asset { get; set; } = string.Empty;
        public DateTime date { get; set; }
        public string subject { get; set; } = string.Empty;
        public string toEmail { get; set; } = string.Empty;

    }
}
