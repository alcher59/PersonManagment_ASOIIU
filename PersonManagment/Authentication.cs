using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PersonManagment.Controllers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using PersonManagment.Data.Models;

namespace PersonManagment.Authentication
{
    public class Authentication
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public Authentication(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<object> Auth(string username, string password)
        {
            
            var result = await _signInManager.PasswordSignInAsync(username, password, true, false);
            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.UserName == username);
                var roles = await _userManager.GetRolesAsync(appUser);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, username),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, roles.Any() ? roles[0] : string.Empty),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var dataTimeNow = DateTime.UtcNow;
                var token = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: dataTimeNow,
                    claims: claims,
                    expires: dataTimeNow.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                );
                string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

                return new { username, token = jwtToken };
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> Registration(RegistrationForm data)
        {
            ApplicationUser user = new ApplicationUser() { 
                UserName = data.userName,
                Email = data.email,
                EmailConfirmed = true,// data.emailConfirmed;
                PhoneNumberConfirmed = false,// data.phoneNumberConfirmed;
                TwoFactorEnabled = false,// data.twoFactorEnabled;
                LockoutEnabled = false,// data.lockoutEnd;
                AccessFailedCount = 0//data.accessFailedCount;
            };
           
            var resultRegistration = await _userManager.CreateAsync(user, data.password);

            if (resultRegistration.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<ApplicationUser> ChangePassword(string userName, string password, string currentPassword)
        {
            ApplicationUser user =  _userManager.Users.SingleOrDefault(x => x.UserName == userName);
            if (user != null)
            {
                await _userManager.ChangePasswordAsync(user, currentPassword, password);
                return user;
            }
            return user;

        }
    }
}
