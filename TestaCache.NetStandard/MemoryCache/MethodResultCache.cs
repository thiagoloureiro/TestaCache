using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.Caching.Memory;

namespace TestaCache.NetStandard.MemoryCache
{
    public class MethodResultCache
    {
        private Microsoft.Extensions.Caching.Memory.MemoryCache _cache;
        private double _cacheRetainSeconds;
        private static readonly Dictionary<string, MethodResultCache> MethodCaches = new Dictionary<string, MethodResultCache>();

        public MethodResultCache(string methodName, int expirationPeriod = 30)
        {
            var options = new MemoryCacheOptions { ExpirationScanFrequency = TimeSpan.FromMinutes(expirationPeriod) };
            _cache = new Microsoft.Extensions.Caching.Memory.MemoryCache(options);
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
            _cache = new Microsoft.Extensions.Caching.Memory.MemoryCache(options);
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