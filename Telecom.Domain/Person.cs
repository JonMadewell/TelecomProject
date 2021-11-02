using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telecom.Domain
{
    public class Person
    {
        public int PersonId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        
        public int AccountId { get; set; }


        public Account Account { get; set; }

        public int LoginId { get; set; }

        public Login Login { get; set; }

        public List<Device> Devices { get; set; }


    }
}
