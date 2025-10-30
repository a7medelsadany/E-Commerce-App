using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.IdentityDTOS
{
    public class UserDto
    {
        public String Email { get; set; }=null!;
        public String DisplayName { get; set; }=null!;
        public String Token { get; set; }=null!;
        
    }
}
