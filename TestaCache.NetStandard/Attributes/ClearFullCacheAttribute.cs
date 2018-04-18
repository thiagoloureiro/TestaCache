using System;
using System.Reflection;
using PostSharp.Aspects;
using PostSharp.Serialization;
using TestaCache.Cache;

namespace TestaCache.NetStandard.Attributes
{
    [PSerializable]
    public class ClearFullCacheAttribute : MethodInterceptionAspect
    {
        public MethodInfo[] _affectedMethods;
        public string _className;

        public ClearFullCacheAttribute(string str)
        {
            _className = str;
        }

        public sealed override void OnInvoke(MethodInterceptionArgs args)
        {
            var classType = Type.GetType(_className);

            _affectedMethods = classType.GetMethods(BindingFlags.Public | BindingFlags.Instance);

            foreach (var mi in _affectedMethods)
                MethodResultCache.GetCache(mi.Name).ClearCachedResults();
        }
    }
}