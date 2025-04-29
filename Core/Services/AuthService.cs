using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Exceptions;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Services.Abstractions;
using Shared;
using Microsoft.IdentityModel;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
namespace Services
{
    public class AuthService(UserManager<AppUser> userManager ,IOptions<JwtOptions> options) : IAuthService
    {

        
        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
           var user = await userManager.FindByEmailAsync(loginDto.Email);
            if(user == null)
            {
                throw new UnAuthorizedException();
            }
          var flag = await   userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!flag) throw new UnAuthorizedException();
            
            //var roles = await userManager.GetRolesAsync(user);
            return new UserResultDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await GenerateJwtTokenAsync(user),
               
            };
        }

        public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
        {

            // Validate Duplication email 

            var CheckUser = await userManager.FindByEmailAsync(registerDto.Email);
            if (CheckUser != null)
            {
                throw new Exception("Email already exists");
            }

            var user = new AppUser()
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                DisplayName = registerDto.DisplayName
            };
           var result = await   userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                throw new ValidationExceptions(errors);
            }

            return new UserResultDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await GenerateJwtTokenAsync(user),

            };

        }

   
        private async Task<string> GenerateJwtTokenAsync(AppUser user)
        {
            var jwtOptions = options.Value;

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
            };

            // Add roles to claims if needed
             var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var secretKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes(jwtOptions.SecretKey) );

            var token = new JwtSecurityToken(

                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: authClaims,
                expires: DateTime.UtcNow.AddDays(jwtOptions.DurationInDays ),
                signingCredentials: new SigningCredentials( secretKey, SecurityAlgorithms.HmacSha256Signature)

            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}