using SmartHome.Domain;

namespace SmartHomeTests.Tests
{
    public static class TestHelpers
    {
        public static SmartHomeCoordinator CreateCoordinator()
        {
            var temp = new TemperatureSensor();
            var light = new LightController();
            var motion = new MotionSensor();
            var door = new DoorLockController();
            var smoke = new SmokeDetector();
            var fan = new FanController();
            var heater = new HeaterController(temp);
            var camera = new CameraController();

            return new SmartHomeCoordinator(
                temp, light, motion, door, smoke, fan, heater, camera
            );
        }
    }
}
