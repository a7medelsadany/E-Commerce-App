using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.IdentityModule
{
    public class Address
    {
        public int Id { get; set; }
        public String FirstName { get; set; } = null!;
        public String LastName { get; set; } = null!;
        public String Street { get; set; } = null!;
        public String City { get; set; } = null!;
        public String Country { get; set; } = null!;

        //---------------------
        //Relation With Application User
        public String UserId { get; set; }
        public ApplicationUser user { get; set; } = null!;
    }
}
