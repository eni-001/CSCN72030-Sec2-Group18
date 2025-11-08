using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Domain
{
    public interface IDevice
    {
        string Name { get; }
        string Status { get; }
    }
}