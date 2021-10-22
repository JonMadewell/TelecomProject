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

        public string PhoneNumber { get; set; }

        public List<Person> People { get; set; }
    }
}
