using System;
using SmartHome.Domain;
using System.Threading;

namespace SmartHome.Demo
{
    internal class Program
    {
        static void Main()
        {
            var sensor = new TemperatureSensor();
            var heater = new HeaterController(sensor);
            var light = new LightController();
            var door = new DoorLockController();
            var fan = new FanController();
            var smoke = new SmokeDetector();
            var motion = new MotionSensor();
            var camera = new CameraController();

            Console.WriteLine("Initial device states:");
            PrintAll(sensor, heater, light, door, fan, smoke, motion, camera);

            Console.WriteLine("\nPerforming actions...");
            light.TurnOn();
            door.Unlock();
            motion.Trigger();
            smoke.Trigger();
            fan.SetSpeed(2);
            camera.Start();
            camera.Snapshot();
            heater.SetSetpoint(24.0);

            for (int i = 0; i < 10; i++)
            {
                heater.Tick();
                Thread.Sleep(150);
                Console.Write(".");
            }
            Console.WriteLine();

            smoke.Clear();
            motion.Clear();

            Console.WriteLine("\nFinal device states:");
            PrintAll(sensor, heater, light, door, fan, smoke, motion, camera);

            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }

        static void PrintAll(
            TemperatureSensor sensor,
            HeaterController heater,
            LightController light,
            DoorLockController door,
            FanController fan,
            SmokeDetector smoke,
            MotionSensor motion,
            CameraController camera)
        {
            Console.WriteLine($"Temperature Sensor: {sensor.Status}");
            Console.WriteLine($"Heater:             {heater.Status}");
            Console.WriteLine($"Light:              {light.Status}");
            Console.WriteLine($"Door:               {door.Status}");
            Console.WriteLine($"Fan:                {fan.Status}");
            Console.WriteLine($"Smoke:              {smoke.Status}");
            Console.WriteLine($"Motion:             {motion.Status}");
            Console.WriteLine($"Camera:             {camera.Status}");
        }
    }
}