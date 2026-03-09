using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNotificationOne.Infrastructure.Redis
{
    public interface IRedisIntegration
    {
        public IDatabase GetDatabase();
    }
}
