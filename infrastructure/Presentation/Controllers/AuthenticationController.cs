using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.DTOS.IdentityDTOS;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiceManager _serviceManager):APIBaseController
    {
        #region Login
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var User = await _serviceManager.AuthenticationServices.LoginAsync(loginDto);
            return Ok(User);
        }
        #endregion

        //-------------------------------
        #region Register
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var User=await _serviceManager.AuthenticationServices.RegisterAsync(registerDto);
            return Ok(User);
        }
        #endregion
    }
}
