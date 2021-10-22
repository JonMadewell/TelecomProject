using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telecom.Domain
{
    public class Plan
    {
        public int PlanId { get; set; }

        public string PlanName { get; set; }

        public int DeviceLimit { get; set; }

        public bool isUnlimited { get; set; }

        public double Price { get; set; }

        public List<Account> Accounts { get; set; }
    }
}
