using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telecom.Domain
{
    public class PersonDevice
    {
        public int PersonDeviceId { get; set; }

        public int PersonId { get; set; }

        public int DeviceId { get; set; }
    }
}
