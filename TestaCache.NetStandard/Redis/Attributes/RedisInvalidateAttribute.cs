using PostSharp.Aspects;
using PostSharp.Serialization;
using StackExchange.Redis;

namespace TestaCache.NetStandard.Redis.Attributes
{
    [PSerializable]
    public class RedisInvalidateAttribute : MethodInterceptionAspect
    {
        public IDatabase _cache;
        public string[] _affectedMethods;

        public RedisInvalidateAttribute(params string[] methods)
        {
            _affectedMethods = methods;
        }

        public sealed override void OnInvoke(MethodInterceptionArgs args)
        {
            _cache = RedisConnectorHelper.Connection.GetDatabase();

            foreach (var mi in _affectedMethods)
            {
                if (Exists(mi))
                    _cache.KeyDeleteAsync(mi);
            }

            base.OnInvoke(args);
        }

        public bool Exists(string key)
        {
            return _cache.KeyExists(key);
        }
    }
}