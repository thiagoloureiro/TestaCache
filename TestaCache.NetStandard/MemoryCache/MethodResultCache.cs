using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace TestaCache.Cache
{
    public class MethodResultCache
    {
        private MemoryCache _cache;
        private double _cacheRetainSeconds;
        private static readonly Dictionary<string, MethodResultCache> MethodCaches = new Dictionary<string, MethodResultCache>();

        public MethodResultCache(string methodName, int expirationPeriod = 30)
        {
            var options = new MemoryCacheOptions { ExpirationScanFrequency = TimeSpan.FromMinutes(expirationPeriod) };
            _cache = new MemoryCache(options);
        }

        public void CacheCallResult(object result, string argument, double _cacheRetainSeconds)
        {
            _cache.Set(argument, result, DateTimeOffset.Now.AddSeconds(_cacheRetainSeconds));
        }

        public object GetCachedResult(string argument)
        {
            return _cache.Get(argument);
        }

        public void ClearCachedResults()
        {
            _cache.Dispose();
            var options = new MemoryCacheOptions { ExpirationScanFrequency = TimeSpan.FromMinutes(30) };
            _cache = new MemoryCache(options);
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