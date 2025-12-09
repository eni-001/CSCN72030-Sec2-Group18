using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Domain
{
    public class SmartHomeCoordinator
    {
        private readonly TemperatureSensor _temp;
        private readonly LightController _light;
        private readonly MotionSensor _motion;
        private readonly DoorLockController _door;
        private readonly SmokeDetector _smoke;
        private readonly FanController _fan;
        private readonly HeaterController _heater;
        private readonly CameraController _camera;

        public TemperatureSensor Temp => _temp;
        public LightController Light => _light;
        public MotionSensor Motion => _motion;
        public DoorLockController Door => _door;
        public SmokeDetector Smoke => _smoke;
        public FanController Fan => _fan;
        public HeaterController Heater => _heater;
        public CameraController Camera => _camera;


        public SmartHomeCoordinator(
            TemperatureSensor temp,
            LightController light,
            MotionSensor motion,
            DoorLockController door,
            SmokeDetector smoke,
            FanController fan,
            HeaterController heater,
            CameraController camera)
        {
            _temp = temp;
            _light = light;
            _motion = motion;
            _door = door;
            _smoke = smoke;
            _fan = fan;
            _heater = heater;
            _camera = camera;
        }

        public void RunSprint2Simulation()
        {

            // 1. Simulate natural fluctuations
            _temp.SimulateFluctuation();

            // 2. Integrate Temperature ↔ Heater ↔ Light ↔ Fan
            _heater.ReactToTemperature();
            _light.AdjustBasedOnTemperature(_temp.CurrentCelsius);

            if (_temp.IsTooHot())
            {
                _fan.SetSpeed(3);
            }
            else if (_temp.IsTooCold())
            {
                _fan.SetSpeed(1);
            }

            // 3. Smoke reaction
            _smoke.Update();
            if (_smoke.SmokeDetected)
            {
                _fan.ReactToSmokeLevel(_smoke.SmokeLevel);
                _light.TurnOn();
            }

            // 4. Motion reaction
            _motion.Update();
            if (_motion.ShouldUnlockDoor())
            {
                _door.ReactToMotion(true);
                _camera.ReactToMotion(true);
                _camera.AutoSnapshotIfNeeded(TimeSpan.FromSeconds(3));
            }
            else
            {
                _camera.Update();
                _door.Update();
            }

        }

        public void PrintAll()
        {
            Console.WriteLine($"Temperature Sensor: {_temp.Status}");
            Console.WriteLine($"Heater:             {_heater.Status}");
            Console.WriteLine($"Light:              {_light.Status}");
            Console.WriteLine($"Door:               {_door.Status}");
            Console.WriteLine($"Fan:                {_fan.Status}");
            Console.WriteLine($"Smoke:              {_smoke.Status}");
            Console.WriteLine($"Motion:             {_motion.Status}");
            Console.WriteLine($"Camera:             {_camera.Status}");
        }
    }
}
