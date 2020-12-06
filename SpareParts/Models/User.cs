using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpareParts.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserPhoto { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

    }
}
