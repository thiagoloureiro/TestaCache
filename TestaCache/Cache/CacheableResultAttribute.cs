using System.Linq;
using System.Security.Principal;
using TestaCache.AOP.Aspects;

namespace TestaCache.Cache
{
    public class CacheableResultAttribute : MethodInterceptionAspect
    {
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

            cache.CacheCallResult(args.ReturnValue, arguments);
        }
    }
}