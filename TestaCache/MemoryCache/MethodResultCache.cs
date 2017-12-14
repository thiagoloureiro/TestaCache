using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Caching;

namespace TestaCache.Cache
{
    public class MethodResultCache
    {
        private readonly string _methodName;
        private MemoryCache _cache;
        private double _cacheRetainSeconds;
        private static readonly Dictionary<string, MethodResultCache> MethodCaches = new Dictionary<string, MethodResultCache>();

        public MethodResultCache(string methodName, int expirationPeriod = 30)
        {
            _methodName = methodName;
            _cache = new MemoryCache(methodName);
        }

        private string GetCacheKey(IEnumerable<object> arguments)
        {
            var key = string.Format(
              $"{_methodName}({string.Join(", ", arguments.Select(x => x != null ? x.ToString() : "<Null>"))})");
            return key;
        }

        public void CacheCallResult(object result, IEnumerable<object> arguments, double _cacheRetainSeconds)
        {
            _cache.Set(GetCacheKey(arguments), result, DateTimeOffset.Now.AddSeconds(_cacheRetainSeconds));
        }

        public object GetCachedResult(IEnumerable<object> arguments)
        {
            return _cache.Get(GetCacheKey(arguments));
        }

        public void ClearCachedResults()
        {
            _cache.Dispose();
            _cache = new MemoryCache(_methodName);
        }

        public static MethodResultCache GetCache(string methodName)
        {
            if (MethodCaches.ContainsKey(methodName))
                return MethodCaches[methodName];
            var cache = new MethodResultCache(methodName);
            MethodCaches.Add(methodName, cache);
            return cache;
        }

        public static MethodResultCache GetCache(MemberInfo methodInfo)
        {
            var methodName = string.Format($"{methodInfo.Name}");
            return GetCache(methodName);
        }
    }
}