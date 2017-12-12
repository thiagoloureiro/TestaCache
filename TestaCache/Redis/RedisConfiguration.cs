using System;
using System.Configuration;

namespace TestaCache.Redis
{
    public static class RedisConfiguration
    {
        public static RedisConfigurationModel GetRedisConfiguration()
        {
            string server = ConfigurationManager.AppSettings["RedisCache_server"];
            string port = ConfigurationManager.AppSettings["RedisCache_port"];
            string name = ConfigurationManager.AppSettings["RedisCache_name"];
            string password = ConfigurationManager.AppSettings["RedisCache_password"];
            string ssl = ConfigurationManager.AppSettings["RedisCache_ssl"];

            var objConfig = new RedisConfigurationModel
            {
                name = name,
                password = password,
                server = server,
                ssl = Convert.ToBoolean(ssl),
                port = int.Parse(port)
            };

            return objConfig;
        }
    }
}