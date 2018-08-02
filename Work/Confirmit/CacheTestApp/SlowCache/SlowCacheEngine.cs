using System.Collections.Generic;
using System.Threading;

namespace SlowCache
{
    public class SlowCacheEngine : ISlowCacheEngine
    {
        public List<CachedSettings> GetCache()
        { 
            Thread.Sleep(3000);
            return new List<CachedSettings>
            {
                new CachedSettings("1", "The First"),
                new CachedSettings("2", "The Second"),
                new CachedSettings("3", "The Third"),
                new CachedSettings("Four", "The Fourth")
            };
        }
    }
}
