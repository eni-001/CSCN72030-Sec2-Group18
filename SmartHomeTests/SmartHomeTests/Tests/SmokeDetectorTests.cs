using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartHome.Domain;

namespace SmartHomeTests.Tests
{
    [TestClass]
    public class SmokeDetectorTests
    {
        [TestMethod]
        public void Smoke_ShouldStartClear()
        {
            var smoke = new SmokeDetector();
            Assert.AreEqual(0, smoke.SmokeLevel);
            Assert.IsFalse(smoke.SmokeDetected);
        }

        [TestMethod]
        public void Smoke_Trigger_ShouldActivateDetection()
        {
            var smoke = new SmokeDetector();
            smoke.Trigger(70);

            Assert.IsTrue(smoke.SmokeDetected);
            Assert.AreEqual(70, smoke.SmokeLevel);
        }

        [TestMethod]
        public void Smoke_IsCritical_ShouldDetectCriticalLevel()
        {
            var smoke = new SmokeDetector();
            smoke.Trigger(90);

            Assert.IsTrue(smoke.IsCritical());
        }
    }
}
