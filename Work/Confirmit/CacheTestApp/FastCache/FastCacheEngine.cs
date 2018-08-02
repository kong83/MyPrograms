using System;
using System.Collections.Generic;
using System.Linq;
using SlowCache;

namespace FastCache
{
    public class FastCacheEngine : IFastCacheEngine
    {
        private readonly int _expirationTimeoutInMs;
        private readonly ISlowCacheEngine _slowCache;

        private DateTime _lastCacheUpdateTime;
        private List<CachedSettings> _cachedSettings;

        public FastCacheEngine(int expirationTimeoutInMs, ISlowCacheEngine slowCache)
        {
            _expirationTimeoutInMs = expirationTimeoutInMs;
            
            _slowCache = slowCache;

            UpdateCache();
        }

        private void UpdateCache()
        {
            _lastCacheUpdateTime = DateTime.Now;

            _cachedSettings = _slowCache.GetCache();
        }

        private bool IsCacheExpired()
        {
            return _lastCacheUpdateTime.AddMilliseconds(_expirationTimeoutInMs) < DateTime.Now;
        }

        public string GetValue(string name)
        {
            if (IsCacheExpired())
            {
                UpdateCache();
            }

            var cacheElement = _cachedSettings.FirstOrDefault(x => x.Name == name);

            if (cacheElement == null)
            {
                return string.Format("Cache doesn't contain element '{0}'", name);
            }

            return cacheElement.Value;
        }
    }
}
