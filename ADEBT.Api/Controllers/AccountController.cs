using ADEBT.Api.Filters;
using ADEBT.Api.Models;
using ADEBT.Api.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ADEBT.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<User> _signinManager;

        public AccountController(IConfiguration configuration,
                                 UserManager<User> userManager,
                                 SignInManager<User> signinManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signinManager = signinManager;
        }

        [HttpPost]
        [ApiValidationFilter]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            User user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
                return Created("", result);

            return BadRequest();
        }

        [HttpPost]
        [ApiValidationFilter]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            User user = await _userManager.FindByNameAsync(login.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, login.Password))
            {

                Claim[] authClaims = new[]                
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                SymmetricSecurityKey authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                DateTime nowUtc = DateTime.Now.ToUniversalTime();
                DateTime expires = nowUtc.AddMinutes(double.Parse(_configuration["Tokens:ExpiryMinutes"])).ToUniversalTime();
                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: _configuration["Tokens:Issuer"],
                    audience: _configuration["Tokens:Audience"],
                    expires: expires,
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    id = user.Id,
                    username = user.UserName,
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            return BadRequest();
        }
    }
}