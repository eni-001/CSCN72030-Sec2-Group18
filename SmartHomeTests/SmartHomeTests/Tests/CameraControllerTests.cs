using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartHome.Domain;
using System;

namespace SmartHomeTests.Tests
{
    [TestClass]
    public class CameraControllerTests
    {
        [TestMethod]
        public void Camera_ShouldStartIdle()
        {
            var cam = new CameraController();
            Assert.IsFalse(cam.IsStreaming);
            Assert.IsNull(cam.LastSnapshotUtc);
        }

        [TestMethod]
        public void Camera_Start_ShouldEnableStreaming()
        {
            var cam = new CameraController();
            cam.Start();

            Assert.IsTrue(cam.IsStreaming);
            Assert.AreEqual("Manual start", cam.LastEvent);
        }

        [TestMethod]
        public void Camera_Stop_ShouldDisableStreaming()
        {
            var cam = new CameraController();
            cam.Start();
            cam.Stop();

            Assert.IsFalse(cam.IsStreaming);
            Assert.AreEqual("Stopped", cam.LastEvent);
        }

        [TestMethod]
        public void Camera_Snapshot_ShouldUpdateTimestamp()
        {
            var cam = new CameraController();
            cam.Start();
            cam.Snapshot();

            Assert.IsNotNull(cam.LastSnapshotUtc);
            Assert.AreEqual("Snapshot taken", cam.LastEvent);
        }

        [TestMethod]
        public void Camera_ReactToMotion_ShouldStartIfAutoMode()
        {
            var cam = new CameraController();
            cam.AutoMode = true;

            cam.ReactToMotion(true);

            Assert.IsTrue(cam.IsStreaming);
            Assert.AreEqual("Started due to motion", cam.LastEvent);
        }

        [TestMethod]
        public void Camera_ReactToMotion_ShouldNotStartIfAutoModeOff()
        {
            var cam = new CameraController();
            cam.AutoMode = false;

            cam.ReactToMotion(true);

            Assert.IsFalse(cam.IsStreaming);
        }

        [TestMethod]
        public void Camera_Update_ShouldAutoStopAfterDelay()
        {
            var cam = new CameraController();
            cam.AutoStopDelay = TimeSpan.FromMilliseconds(10);

            cam.Start();
            Thread.Sleep(20); // allow time to pass

            cam.Update();

            Assert.IsFalse(cam.IsStreaming);
            Assert.AreEqual("Auto-stop (timeout)", cam.LastEvent);
        }
    }
}
