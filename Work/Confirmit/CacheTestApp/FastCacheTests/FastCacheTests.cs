using System.Diagnostics;
using System.Threading;
using FastCache;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlowCache;

namespace FastCacheTests
{
    [TestClass]
    public class FastCacheTests
    {
        [TestMethod]
        public void GetValue_ValueIsCorrect()
        {
            int expirationTimeoutInMs = 10;
            string testCachedName = "1";

            IFastCacheEngine fastCache = new FastCacheEngine(expirationTimeoutInMs, new FakeSlowCacheEngine());
            string value = fastCache.GetValue(testCachedName);

            Assert.AreEqual("The First", value, "GetValue works incorrect");
        }

        [TestMethod]
        public void GetValue_Get2ValuesDuringExpirationTimeout_FastCacheDoesNotUpdateCache()
        {
            int waytTimeoutInMs = 1000;
            string testCachedName1 = "1";
            string testCachedName2 = "2";

            IFastCacheEngine fastCache = new FastCacheEngine(waytTimeoutInMs * 10, new FakeSlowCacheEngine(waytTimeoutInMs));
            string value1 = fastCache.GetValue(testCachedName1);
            Thread.Sleep(waytTimeoutInMs / 10);
            
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            string value2 = fastCache.GetValue(testCachedName2);
            stopwatch.Stop();

            Assert.AreEqual("The First", value1, "GetValue works incorrect");
            Assert.AreEqual("The Second", value2, "GetValue works incorrect");
            Assert.IsTrue(stopwatch.ElapsedMilliseconds < waytTimeoutInMs, "FastCache updates cache incorrectly");
        }

        [TestMethod]
        public void GetValue_GetValueAfterExpirationTimeout_FastCacheDoUpdateCache()
        {
            int waytTimeoutInMs = 100;
            string testCachedName1 = "1";
            string testCachedName2 = "2";

            IFastCacheEngine fastCache = new FastCacheEngine(waytTimeoutInMs * 10, new FakeSlowCacheEngine(waytTimeoutInMs));
            string value1 = fastCache.GetValue(testCachedName1);
            Thread.Sleep(waytTimeoutInMs * 10);

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            string value2 = fastCache.GetValue(testCachedName2);
            stopwatch.Stop();

            Assert.AreEqual("The First", value1, "GetValue works incorrect");
            Assert.AreEqual("The Second", value2, "GetValue works incorrect");
            Assert.IsTrue(stopwatch.ElapsedMilliseconds >= waytTimeoutInMs, "FastCache updates cache incorrectly");
        }
    }
}
