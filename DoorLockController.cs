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
        public string Status => IsLocked ? "Locked" : "Unlocked";

        public DoorLockController(string name = "Main Door")
        {
            Name = name;
            IsLocked = true;
        }

        public void Lock() => IsLocked = true;
        public void Unlock() => IsLocked = false;
    }
}
