using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Quotes.Application.ViewModels.Authenticate;
using Quotes.Domain.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<IdentityUser<int>> userManager;
        private readonly IConfiguration _configuration;

        public AuthenticateController(UserManager<IdentityUser<int>> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]LoginModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized(new Response { Status = "Error", Message = "Неправильный логин!" });
            }
            if (await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("UserId", user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    Position = userRoles,
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            return Unauthorized(new Response { Status = "Error", Message = "Неправильный пароль!" });
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (model.Password == model.ConfirmPassword && model.Password != null)
            {
                var userExists = await userManager.FindByEmailAsync(model.Email);
                if (userExists != null)
                {
                    return Unauthorized(new Response { Status = "Error", Message = "Пользователь с таким логин существует!" });
                }

                IdentityUser<int> user = new IdentityUser<int>()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Email,
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "custom");
                    return Ok(new Response { Status = "Success", Message = "Пользователь успешно добавлен!" });
                }
                else
                {
                    var errorMessages = result.Errors.FirstOrDefault();
                    return Unauthorized(new Response { Status = errorMessages.Code, Message = errorMessages.Description });
                }
            }
            else
            {
                return Unauthorized(new Response { Status = "Password", Message = "Пароль не совподает!" });
            }
        }


    }
}