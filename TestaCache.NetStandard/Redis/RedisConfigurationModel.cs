namespace TestaCache.NetStandard.Redis
{
    public class RedisConfigurationModel
    {
        public string server { get; set; }
        public int port { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public bool ssl { get; set; }
    }
}