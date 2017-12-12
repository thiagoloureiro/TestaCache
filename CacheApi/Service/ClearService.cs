using TestaCache.Redis.Attributes;

namespace CacheApi.Service
{
    public class ClearService
    {
        [RedisClearFullCache]
        public bool ClearAll()
        {
            return true;
        }
    }
}