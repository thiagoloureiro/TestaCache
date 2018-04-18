using PostSharp.Aspects;
using PostSharp.Serialization;
using TestaCache.Cache;

namespace TestaCache.NetStandard.Attributes
{
    [PSerializable]
    public class CacheableResultAttribute : MethodInterceptionAspect
    {
        public double _cacheRetainSeconds;

        public CacheableResultAttribute(params double[] cacheRetainSeconds)
        {
            _cacheRetainSeconds = cacheRetainSeconds[0];
        }

        public sealed override void OnInvoke(MethodInterceptionArgs args)
        {
            var cache = MethodResultCache.GetCache(args.Method);
            var result = cache.GetCachedResult(args.Method.ToString());
            if (result != null)
            {
                args.ReturnValue = result;
                return;
            }

            base.OnInvoke(args);

            cache.CacheCallResult(args.ReturnValue, args.Method.ToString(), _cacheRetainSeconds);
        }
    }
}