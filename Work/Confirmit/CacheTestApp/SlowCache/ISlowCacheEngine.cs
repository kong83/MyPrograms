using System.Collections.Generic;

namespace SlowCache
{
    public interface ISlowCacheEngine
    {
        List<CachedSettings> GetCache();
    }
}
