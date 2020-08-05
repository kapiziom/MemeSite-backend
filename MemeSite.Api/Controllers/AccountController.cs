using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using MemeSite.Api;
using MemeSite.Domain.Models;
using MemeSite.Application.Interfaces;
using MemeSite.Application.ViewModels;

namespace MemeSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<PageUser> _userManager;
        private readonly ApplicationSettings _appSettings;
        private readonly IUserService _userService;

        public AccountController(IOptions<ApplicationSettings> appSettings,
            UserManager<PageUser> userManager,
            IUserService userService)
        {
            _appSettings = appSettings.Value;
            _userManager = userManager;
            _userService = userService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginVM model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                //get assigned role
                var role = await _userManager.GetRolesAsync(user);
                IdentityOptions _options = new IdentityOptions();

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID", user.Id.ToString()),
                        new Claim("userName", user.UserName),
                        new Claim("userRole", role.FirstOrDefault()),
                        new Claim(_options.ClaimsIdentity.RoleClaimType, role.FirstOrDefault())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            else
                return BadRequest(new { message = "Username or password is incorrect." });
        }

        [HttpPost("Register")]
        public async Task<object> Register([FromBody] RegisterVM model)
            => await _userService.RegisterUser(model);
        

        [HttpPut("ChangePassword")]
        [Authorize]
        public async Task<object> ChangePassword([FromBody] ChangePasswordVM changePasswordVM)
            => await _userService.ChangePassword(changePasswordVM, User);

        [HttpPut("ChangeEmail")]
        [Authorize]
        public async Task<object> ChangeEmail([FromBody] ChangeEmailVM changeEmailVM)
            => await _userService.ChangeEmail(changeEmailVM, User);

        [HttpPut("SetRole")]
        [Authorize(Roles = "Administrator")]
        public async Task<object> SetRole([FromBody] SetUserRoleVM setRole)
            => await _userService.SetUserRole(setRole, User);


    }
}