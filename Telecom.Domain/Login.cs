using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telecom.Domain
{
    public class Login
    {
        public int LoginId { get; set; }
        public int PersonId { get; set; }

        public Person Person { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
