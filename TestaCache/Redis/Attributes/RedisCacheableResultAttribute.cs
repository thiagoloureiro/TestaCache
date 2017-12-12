using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PostSharp.Aspects;

namespace TestaCache.Redis.Attributes
{
    [Serializable]
    public class RedisCacheableResultAttribute : MethodInterceptionAspect
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects };

        public sealed override void OnInvoke(MethodInterceptionArgs args)
        {
            var cache = RedisConnectorHelper.Connection.GetDatabase();
            var result = cache.StringGet(args.Method.Name);

            if (result.HasValue)
            {
                var ret = Deserialize(result);

                args.ReturnValue = ret;
                return;
            }

            base.OnInvoke(args);

            cache.StringSet(args.Method.Name, Serialize(args.ReturnValue));
        }

        private static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, Settings);
        }

        private static dynamic Deserialize(string data)
        {
            return JsonConvert.DeserializeObject<List<dynamic>>(data, Settings);
        }
    }
}