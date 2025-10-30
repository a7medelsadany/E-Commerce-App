using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTOS.IdentityDTOS;

namespace Services.Abstractions
{
    public interface IAuthenticationServices
    {
        public Task<UserDto> LoginAsync(LoginDto loginDto);
        public Task<UserDto> RegisterAsync (RegisterDto registerDto);
    }
}
