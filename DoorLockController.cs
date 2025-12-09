using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Domain
{
    public class DoorLockController : IDevice
    {
        public string Name { get; }
        public bool IsLocked { get; private set; }

        // Sprint 2: Extra information for GUI + logic
        public string LastAction { get; private set; } = "";
        public TimeSpan AutoLockDelay { get; set; } = TimeSpan.FromSeconds(10);
        public bool SilentMode { get; set; } = false;  // no sound in sprint 3

        private DateTime? _lastUnlockedTimeUtc;

        public string Status =>
            IsLocked ? $"Locked ({LastAction})" : $"Unlocked ({LastAction})";

        public DoorLockController(string name = "Main Door")
        {
            Name = name;
            IsLocked = true;
            LastAction = "Initial Lock";
        }

        // Sprint 1 (keep)
        public void Lock()
        {
            IsLocked = true;
            LastAction = "Locked manually";
        }

        // Sprint 1 (keep)
        public void Unlock()
        {
            IsLocked = false;
            LastAction = "Unlocked manually";
            _lastUnlockedTimeUtc = DateTime.UtcNow;
        }

        // Sprint 2: Auto-lock after inactivity
        public void Update()
        {
            if (!IsLocked && _lastUnlockedTimeUtc.HasValue)
            {
                if (DateTime.UtcNow - _lastUnlockedTimeUtc > AutoLockDelay)
                {
                    IsLocked = true;
                    LastAction = "Auto-locked (timeout)";
                }
            }
        }

        // Sprint 2: Unlock when motion is detected
        public void ReactToMotion(bool motionDetected)
        {
            if (motionDetected)
            {
                Unlock();
                LastAction = "Unlocked due to motion";
                _lastUnlockedTimeUtc = DateTime.UtcNow;
            }
        }
    }
}
