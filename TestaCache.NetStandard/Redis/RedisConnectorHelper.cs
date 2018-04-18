using StackExchange.Redis;
using System;

namespace TestaCache.NetStandard.Redis
{
    public class RedisConnectorHelper
    {
        static RedisConnectorHelper()
        {
            var config = RedisConfiguration.GetRedisConfiguration();

            lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
          {
              var str = $"{config.server}:{config.port},name={config.name},password={config.password},allowAdmin=true";
              return ConnectionMultiplexer.Connect(str);
          });
        }

        private static readonly Lazy<ConnectionMultiplexer> lazyConnection;

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}