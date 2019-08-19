using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace AuthService.Core.Redis
{
    public class RedisHelper
    {

        protected ConnectionMultiplexer multiplexer;
        public RedisHelper(IConfiguration configuration)
        {
            this.multiplexer = ConnectionMultiplexer.Connect(configuration.GetSection("RedisCaching:ConnectionString").Value);
        }
    }
}
