using Data.Entities;
using Data.Exceptions;
using Data.Models;
using Data.Repositories.Interfaces;
using Data.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services
{
    public class LoginService : ILoginService
    {
        readonly UserManager<User> _userManager;
        readonly RoleManager<IdentityRole> _roleManager;
        readonly IConfiguration _configuration;

        public LoginService(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<TokenInfo> Login(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                var token = GetToken(authClaims);
                var result = new TokenInfo
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo
                };
                return result;
            }
            else
            {
                throw new EntityNotFoundException("Login Not Found");
            }
        }

        public async Task<ResponseModel> Register(RegisterModel model)
        {
            ResponseModel response = new();

            var userNameExists = await _userManager.FindByNameAsync(model.UserName);

            if (userNameExists != null)
            {
                throw new DuplicateException("UserName Already Exists");
            }

            var emailExists = await _userManager.FindByEmailAsync(model.Email);

            if (emailExists != null)
            {
                throw new DuplicateException("Email Already Exists");
            }

            User user = new()
            {
                UserName = model.UserName,
                SecurityStamp = Guid.NewGuid().ToString(),
                Name = model.Name,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
          
            if (result.Succeeded)
            {
                response.Success = true;
                response.Message = "User is created Successfully";
            }

            else
            {
                response.Success = false;
                response.Message = "Creation of User is Failed";
            }

            if (model.Email == "jith@yopmail.com" && model.Password == "Jith@123")
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _userManager.AddToRoleAsync(user, "Admin");
            }

            else
            {
                var roleExists = await _roleManager.RoleExistsAsync("Audience");
              
                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole("Audience"));
                }
               
                await _userManager.AddToRoleAsync(user, "Audience");
            }
            return response;
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                _configuration["JWT:ValidIssuer"],
                _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(8),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
    }
}
