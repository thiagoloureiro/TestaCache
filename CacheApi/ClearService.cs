using TestaCache.Redis.Attributes;

namespace CacheApi
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