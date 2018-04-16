using PostSharp.Aspects;
using PostSharp.Serialization;

namespace TestaCache.Redis.Attributes
{
    [PSerializable]
    public class RedisClearFullCacheAttribute : MethodInterceptionAspect
    {
        public sealed override void OnInvoke(MethodInterceptionArgs args)
        {
            Clear();
            base.OnInvoke(args);
        }

        public void Clear()
        {
            var connectionMultiplexer = RedisConnectorHelper.Connection;

            var endpoints = connectionMultiplexer.GetEndPoints(true);
            foreach (var endpoint in endpoints)
            {
                var server = connectionMultiplexer.GetServer(endpoint);
                server.FlushAllDatabasesAsync();
            }
        }
    }
}