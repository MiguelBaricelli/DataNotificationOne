using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNotificationOne.Domain.Models.BraApi
{
    public class BrApiRequest
    {
        public required List<BrApiModel> BraApiModels { get; set; }
        public required string RequestAt { get; set; }
    }
}
