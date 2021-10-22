using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telecom.Domain
{
    public class Account
    {
        public int AccountId { get; set; }

        public int PersonId { get; set; }

        public Person Person { get; set; }

        public List<Plan> plans { get; set; }

    }
}
