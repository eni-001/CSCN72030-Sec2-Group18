using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Domain
{
    public class MotionSensor : IDevice
    {
        public string Name { get; }
        public bool Detected { get; private set; }
        public DateTime? LastDetectedUtc { get; private set; }
        public string Status => Detected
            ? $"Motion detected at {LastDetectedUtc:HH:mm:ss} UTC"
            : "No motion";

        public MotionSensor(string name = "Hallway Motion")
        {
            Name = name;
        }

        public void Trigger()
        {
            Detected = true;
            LastDetectedUtc = DateTime.UtcNow;
        }

        public void Clear() => Detected = false;
    }
}
