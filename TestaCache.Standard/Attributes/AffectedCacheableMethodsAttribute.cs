using PostSharp.Aspects;
using PostSharp.Serialization;

namespace TestaCache.Cache
{
    [PSerializable]
    public class AffectedCacheableMethodsAttribute : MethodInterceptionAspect
    {
        public string[] _affectedMethods;

        public AffectedCacheableMethodsAttribute(params string[] methods)
        {
            _affectedMethods = methods;
        }

        public override void OnInvoke(MethodInterceptionArgs args)
        {
            base.OnInvoke(args);

            foreach (var mi in _affectedMethods)
                MethodResultCache.GetCache(mi).ClearCachedResults();
        }
    }
}