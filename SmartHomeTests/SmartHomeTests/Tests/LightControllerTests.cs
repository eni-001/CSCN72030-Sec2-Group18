using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartHome.Domain;

namespace SmartHomeTests.Tests
{
    [TestClass]
    public class LightControllerTests
    {
        [TestMethod]
        public void Light_ShouldStartOff()
        {
            var light = new LightController();
            Assert.IsFalse(light.IsOn);
        }

        [TestMethod]
        public void Light_Toggle_ShouldSwitchState()
        {
            var light = new LightController();

            light.Toggle();
            Assert.IsTrue(light.IsOn);

            light.Toggle();
            Assert.IsFalse(light.IsOn);
        }

        [TestMethod]
        public void Light_SetBrightness_ShouldClampCorrectly()
        {
            var light = new LightController();

            light.SetBrightness(150);
            Assert.AreEqual(100, light.BrightnessLevel);

            light.SetBrightness(-20);
            Assert.AreEqual(0, light.BrightnessLevel);
        }
    }
}
