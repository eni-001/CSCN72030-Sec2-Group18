using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartHome.Domain;

namespace SmartHomeTests.Tests
{
    [TestClass]
    public class SmartHomeCoordinatorTests
    {
        [TestMethod]
        public void Coordinator_ShouldInitializeAllDevices()
        {
            var home = TestHelpers.CreateCoordinator();

            Assert.IsNotNull(home.Temp);
            Assert.IsNotNull(home.Light);
            Assert.IsNotNull(home.Motion);
            Assert.IsNotNull(home.Door);
            Assert.IsNotNull(home.Smoke);
            Assert.IsNotNull(home.Fan);
            Assert.IsNotNull(home.Heater);
            Assert.IsNotNull(home.Camera);
        }

        [TestMethod]
        public void Coordinator_Run_ShouldNotThrow()
        {
            var home = TestHelpers.CreateCoordinator();

            home.RunSprint2Simulation();

            Assert.IsTrue(true); // If no exception → PASS
        }
    }
}
