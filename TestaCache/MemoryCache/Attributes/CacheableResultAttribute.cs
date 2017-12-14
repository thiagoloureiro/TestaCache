using System;
using PostSharp.Aspects;
using System.Linq;
using System.Security.Principal;

namespace TestaCache.Cache
{
    [Serializable]
    public class CacheableResultAttribute : MethodInterceptionAspect
    {
        private double _cacheRetainSeconds;

        public CacheableResultAttribute(params double[] cacheRetainSeconds)
        {
            _cacheRetainSeconds = cacheRetainSeconds[0];
        }

        public sealed override void OnInvoke(MethodInterceptionArgs args)
        {
            var cache = MethodResultCache.GetCache(args.Method);
            var arguments = args.Arguments.Union(new[] { WindowsIdentity.GetCurrent().Name }).ToList();
            var result = cache.GetCachedResult(arguments);
            if (result != null)
            {
                args.ReturnValue = result;
                return;
            }

            base.OnInvoke(args);

            cache.CacheCallResult(args.ReturnValue, arguments, _cacheRetainSeconds);
        }
    }
}