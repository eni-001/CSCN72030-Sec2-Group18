using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartHome.Domain;

namespace SmartHomeTests.Tests
{
    [TestClass]
    public class FanControllerTests
    {
        [TestMethod]
        public void Fan_ShouldStartOff()
        {
            var fan = new FanController();
            Assert.AreEqual(0, fan.Speed);
        }

        [TestMethod]
        public void Fan_SetSpeed_ShouldUpdateWithinRange()
        {
            var fan = new FanController();
            fan.SetSpeed(2);

            Assert.AreEqual(2, fan.Speed);
        }

        [TestMethod]
        public void Fan_Off_ShouldSetSpeedZero()
        {
            var fan = new FanController();
            fan.SetSpeed(3);
            fan.Off();

            Assert.AreEqual(0, fan.Speed);
        }
    }
}
