using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartHome.Domain;

namespace SmartHomeTests.Tests
{
    [TestClass]
    public class DoorLockControllerTests
    {
        [TestMethod]
        public void Door_ShouldStartLocked()
        {
            var door = new DoorLockController();
            Assert.IsTrue(door.IsLocked);
        }

        [TestMethod]
        public void Door_Unlock_ShouldUnlock()
        {
            var door = new DoorLockController();
            door.Unlock();
            Assert.IsFalse(door.IsLocked);
        }

        [TestMethod]
        public void Door_Lock_ShouldLock()
        {
            var door = new DoorLockController();
            door.Unlock();   // first unlock
            door.Lock();
            Assert.IsTrue(door.IsLocked);
        }
    }
}
