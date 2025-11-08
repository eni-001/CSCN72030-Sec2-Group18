using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Domain
{
    public class CameraController : IDevice
    {
        public string Name { get; }
        public bool IsStreaming { get; private set; }
        public DateTime? LastSnapshotUtc { get; private set; }
        public string Status => IsStreaming
            ? $"Streaming (snapshot {LastSnapshotUtc:HH:mm:ss} UTC)"
            : "Idle";

        public CameraController(string name = "Front Door Camera")
        {
            Name = name;
        }

        public void Start() => IsStreaming = true;
        public void Stop() => IsStreaming = false;
        public void Snapshot() => LastSnapshotUtc = DateTime.UtcNow;
    }
}