using TestaCache.NetStandard.Redis.Attributes;

namespace API.NetCore.Service
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