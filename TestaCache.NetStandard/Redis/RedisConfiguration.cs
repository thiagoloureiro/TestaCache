using System;

namespace TestaCache.NetStandard.Redis
{
    public static class RedisConfiguration
    {
        public static string name { get; set; } = "localhost";
        public static string password { get; set; }
        public static string server { get; set; } = "localhost";
        public static bool ssl { get; set; } = false;
        public static int port { get; set; }

        public static RedisConfigurationModel GetRedisConfiguration()
        {
            var objConfig = new RedisConfigurationModel
            {
                name = name,
                password = password,
                server = server,
                ssl = Convert.ToBoolean(ssl),
                port = port
            };

            return objConfig;
        }
    }
}