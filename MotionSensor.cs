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

        // Sprint 1 fields (keep)
        public bool Detected { get; private set; }
        public DateTime? LastDetectedUtc { get; private set; }
        public string Status => Detected
            ? $"Motion detected at {LastDetectedUtc:HH:mm:ss} UTC"
            : "No motion";

        // Sprint 2: Sensitivity + Auto-Clear
        public int Sensitivity { get; set; } = 5; // 1–10
        public TimeSpan AutoClearDelay { get; set; } = TimeSpan.FromSeconds(10);

        // Sprint 2: Active mode for integration
        public bool IsActive { get; set; } = true;

        public MotionSensor(string name = "Hallway Motion")
        {
            Name = name;
        }

        // Sprint 1 (keep)
        public void Trigger()
        {
            if (!IsActive) return;

            Detected = true;
            LastDetectedUtc = DateTime.UtcNow;
        }

        // Sprint 1 (keep)
        public void Clear() => Detected = false;

        // Sprint 2: Automatically clear motion after delay
        public void Update()
        {
            if (Detected && LastDetectedUtc.HasValue)
            {
                if (DateTime.UtcNow - LastDetectedUtc > AutoClearDelay)
                {
                    Detected = false;
                }
            }
        }

        // Sprint 2: Helper for integration (door/camera logic)
        public bool ShouldUnlockDoor() => Detected;
    }
}
