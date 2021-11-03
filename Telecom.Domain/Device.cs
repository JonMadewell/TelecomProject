using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telecom.Domain
{
    public class Device
    {
        public int DeviceId { get; set; }

        public string DeviceName { get; set; }

        public string Camera { get; set; }

        public string Storage { get; set; }
        public string DeviceDis { get; set; }

        
        public List<Person> People { get; set; }
    }
}
