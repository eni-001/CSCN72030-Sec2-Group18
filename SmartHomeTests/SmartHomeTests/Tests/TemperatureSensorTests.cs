using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartHome.Domain;

namespace SmartHomeTests.Tests
{
    [TestClass]
    public class TemperatureSensorTests
    {
        [TestMethod]
        public void TemperatureSensor_DefaultCelsius_ShouldBeValid()
        {
            var sensor = new TemperatureSensor();
            Assert.IsTrue(sensor.CurrentCelsius >= 0 && sensor.CurrentCelsius <= 40);
        }

        [TestMethod]
        public void TemperatureSensor_Set_ShouldChangeValue()
        {
            var sensor = new TemperatureSensor();
            sensor.Set(25.0);
            Assert.AreEqual(25.0, sensor.CurrentCelsius);
        }

        [TestMethod]
        public void TemperatureSensor_Nudge_ShouldAddDelta()
        {
            var sensor = new TemperatureSensor(start: 20.0);
            sensor.Nudge(2.5);
            Assert.AreEqual(22.5, sensor.CurrentCelsius);
        }

        [TestMethod]
        public void TemperatureSensor_IsTooHot_ShouldDetectCorrectly()
        {
            var sensor = new TemperatureSensor(start: 30);
            Assert.IsTrue(sensor.IsTooHot());
        }

        [TestMethod]
        public void TemperatureSensor_IsTooCold_ShouldDetectCorrectly()
        {
            var sensor = new TemperatureSensor(start: 10);
            Assert.IsTrue(sensor.IsTooCold());
        }
    }
}
