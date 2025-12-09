using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartHome.Domain;

namespace SmartHomeTests.Tests
{
    [TestClass]
    public class MotionSensorTests
    {
        [TestMethod]
        public void Motion_ShouldStartClear()
        {
            var sensor = new MotionSensor();
            Assert.IsFalse(sensor.Detected);
        }

        [TestMethod]
        public void Motion_Trigger_ShouldSetDetected()
        {
            var sensor = new MotionSensor();
            sensor.Trigger();
            Assert.IsTrue(sensor.Detected);
        }

        [TestMethod]
        public void Motion_Clear_ShouldRemoveDetection()
        {
            var sensor = new MotionSensor();
            sensor.Trigger();
            sensor.Clear();
            Assert.IsFalse(sensor.Detected);
        }
    }
}
