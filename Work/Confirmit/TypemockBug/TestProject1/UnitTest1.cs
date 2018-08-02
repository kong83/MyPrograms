using System.Diagnostics;
using Confirmit.CATI.Core.DAL.Generated.Cache;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;
using TypemockBug;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var testClass = BvSurveyCache.Instance;
            if (testClass == null)
            {
                Trace.TraceInformation("testClass is null");
            }
            else
            {
                Trace.TraceInformation("testClass is normal");
            }

            Isolate.WhenCalled(() => testClass.OnTableChanged()).IgnoreCall();

            Assert.IsTrue(true);
        }
    }
}
