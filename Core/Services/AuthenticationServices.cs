using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities.IdentityModule;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Abstractions;
using Shared.DTOS.IdentityDTOS;

namespace Services
{
    public class AuthenticationServices(UserManager<ApplicationUser> _userManager,IConfiguration configuration) : IAuthenticationServices
    {
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            //check if user exists
            var User = await _userManager.FindByEmailAsync(loginDto.Email);
            if(User is null)
            {
                throw new UserNotFoundException(loginDto.Email);
            }
            var IsPasswordValid = await _userManager.CheckPasswordAsync(User, loginDto.Password);
            if (IsPasswordValid)
            {
                return new UserDto()
                {
                    DisplayName = User.DisplayName,
                    Email = User.Email,
                    Token =await CreateTokenAsync(User)
                };
            }
            else
            {
                throw new UnauthorizedException();
            }
        }

        //------------------------------------------------

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            //Mapping To Application User
            var User = new ApplicationUser()
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber
            };
            //create user
            var Result=await _userManager.CreateAsync(User, registerDto.Password);
            //check
            if (Result.Succeeded)
            {
                return new UserDto()
                {
                    DisplayName = User.DisplayName,
                    Email = User.Email,
                    Token =await CreateTokenAsync(User)
                };
            }
            else
            {
                var Errors = Result.Errors.Select(E => E.Description).ToList();
                throw new BadRequestException(Errors);
            }
        }


        //--------------------------------------------------
        private async Task<String> CreateTokenAsync(ApplicationUser user)
        {
            var Claims = new List<Claim>()
            {
                new (ClaimTypes.Email,user.Email!),
                new (ClaimTypes.Name,user.UserName!),
                new(ClaimTypes.NameIdentifier,user.Id)
            };
            var Roles = await _userManager.GetRolesAsync(user);
            //add roles to claims
            foreach(var Role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, Role));
            }
            //------------------
            //create Secret Key
            var SecretKey = configuration.GetSection("JWTOptions")["SecretKey"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            //--------------------
            //create Token
            var Token = new JwtSecurityToken(
               issuer: configuration["JWTOptions.Issuer"],
               audience: configuration["JWTOptions.Audience"],
               claims: Claims,
               expires: DateTime.Now.AddHours(1),
               signingCredentials: Creds
                );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
