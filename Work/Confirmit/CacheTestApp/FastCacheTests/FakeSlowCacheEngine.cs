using System.Collections.Generic;
using System.Threading;
using SlowCache;

namespace FastCacheTests
{
    class FakeSlowCacheEngine : ISlowCacheEngine
    {
        private readonly int _waytTime;
        public FakeSlowCacheEngine()
            : this (0)
        {
            
        }

        public FakeSlowCacheEngine(int waytTime)
        {
            _waytTime = waytTime;
        }

        public List<CachedSettings> GetCache()
        {
            Thread.Sleep(_waytTime);

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
