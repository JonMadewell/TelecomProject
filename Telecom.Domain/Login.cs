using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
