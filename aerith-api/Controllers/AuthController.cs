using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Aerith.Api.Interfaces;
using Aerith.Common.Models.Dto;
using Aerith.Common.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Aerith.Api.Controllers
{
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(IJwtService jwtService, UserManager<ApplicationUser> userManager)
        {
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        [HttpPost("login")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> Login_1_0([FromBody] CredentialsDto credentials, ApiVersion apiVersion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ClaimsIdentity claimsIdentity = await GetClaimsIdentityAsync(credentials);

            if(claimsIdentity == null)
            {
                return Unauthorized();
            }

            var token = new TokenDto(){
                Token = await _jwtService.CreateJwt(claimsIdentity)
            };

            return Ok(token);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentityAsync(CredentialsDto credentials)
        {
            ApplicationUser applicationUser = await _userManager.FindByNameAsync(credentials.Username);

            if (applicationUser == null)
            {
                return await Task.FromResult<ClaimsIdentity>(null);
            }

            if (await _userManager.CheckPasswordAsync(applicationUser, credentials.Password))
            {
                return await Task.FromResult(GetClaimsIdentity(applicationUser));
            }

            return await Task.FromResult<ClaimsIdentity>(null);
        }

        private ClaimsIdentity GetClaimsIdentity(ApplicationUser applicationUser)
        {
            return new ClaimsIdentity(new GenericIdentity(applicationUser.UserName), new[]
            {
                new Claim(ClaimTypes.Name, applicationUser.UserName)
            });
        }
    }
}