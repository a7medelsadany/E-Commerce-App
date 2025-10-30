using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.IdentityDTOS
{
    public class RegisterDto
    {
        [EmailAddress]
        public String Email { get; set; }=null!;
        public String Password { get; set; } = null!;
        public String  UserName { get; set; }=null!;
        public String  DisplayName { get; set; }=null!;
        public String  PhoneNumber { get; set; }=null!;
    }
}
