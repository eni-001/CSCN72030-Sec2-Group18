using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Domain
{
    public class SmokeDetector : IDevice
    {
        public string Name { get; }

        // Sprint 1 fields (keep)
        public bool SmokeDetected { get; private set; }
        public string Status => SmokeDetected
            ? $"SMOKE ALERT (Level {SmokeLevel})"
            : "Clear";

        // ✅ Sprint 2: New fields
        public int SmokeLevel { get; private set; } = 0; // 0–100
        public DateTime? LastSmokeUtc { get; private set; }
        public TimeSpan AutoClearDelay { get; set; } = TimeSpan.FromSeconds(20);
        public int CriticalLevel { get; set; } = 80;

        public SmokeDetector(string name = "Kitchen Smoke")
        {
            Name = name;
        }

        // Sprint 1 behaviour (basic trigger)
        public void Trigger()
        {
            SmokeDetected = true;
            SmokeLevel = 50;
            LastSmokeUtc = DateTime.UtcNow;
        }

        // ✅ Sprint 2: Overload to trigger with a level
        public void Trigger(int level)
        {
            SmokeDetected = true;
            SmokeLevel = Math.Clamp(level, 0, 100);
            LastSmokeUtc = DateTime.UtcNow;
        }

        public void Clear()
        {
            SmokeDetected = false;
            SmokeLevel = 0;
        }

        // ✅ Sprint 2: Auto-clear after some time
        public void Update()
        {
            if (SmokeDetected && LastSmokeUtc.HasValue)
            {
                if (DateTime.UtcNow - LastSmokeUtc > AutoClearDelay)
                {
                    Clear();
                }
            }
        }

        // ✅ Sprint 2: Helper for integration (fan, alarm, etc.)
        public bool IsCritical() => SmokeLevel >= CriticalLevel;
    }
}
