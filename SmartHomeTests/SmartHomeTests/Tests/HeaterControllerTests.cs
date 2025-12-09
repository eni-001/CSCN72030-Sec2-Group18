using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartHome.Domain;

namespace SmartHomeTests.Tests
{
    [TestClass]
    public class HeaterControllerTests
    {
        [TestMethod]
        public void Heater_ShouldStartWithDefaultSetpoint()
        {
            var sensor = new TemperatureSensor();
            var heater = new HeaterController(sensor);

            Assert.AreEqual(22.0, heater.SetpointC);
        }

        [TestMethod]
        public void Heater_SetSetpoint_ShouldChangeValue()
        {
            var heater = new HeaterController(new TemperatureSensor());
            heater.SetSetpoint(25);

            Assert.AreEqual(25, heater.SetpointC);
        }

        [TestMethod]
        public void Heater_SetPower_ShouldClampRange()
        {
            var heater = new HeaterController(new TemperatureSensor());

            heater.SetPower(10);
            Assert.AreEqual(5, heater.PowerLevel);

            heater.SetPower(0);
            Assert.AreEqual(1, heater.PowerLevel);
        }
    }
}
