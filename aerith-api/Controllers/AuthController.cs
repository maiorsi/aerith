using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Aerith.Api.Interfaces;
using Aerith.Api.Settings;
using Aerith.Common.Models.Dto;
using Aerith.Common.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Aerith.Api.Controllers
{
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly JwtSettings _jwtSettings;

        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(
            IJwtService jwtService,
            IOptions<JwtSettings> jwtSettings,
            UserManager<ApplicationUser> userManager)
        {
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
            _jwtSettings = jwtSettings.Value ?? throw new ArgumentNullException(nameof(jwtSettings));
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

            var user = await _userManager.FindByNameAsync(claimsIdentity.Name);

            if(user == null)
            {
                return Unauthorized();
            }

            var refreshToken = await _jwtService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddMinutes(_jwtSettings.RefreshTokenExpiryMinutes);
            user.SecurityStamp = Guid.NewGuid().ToString();

            var identityResult = await _userManager.UpdateAsync(user);

            if(!identityResult.Succeeded)
            {
                return Unauthorized();
            }

            var token = new TokenDto(){
                Token = await _jwtService.CreateJwt(claimsIdentity),
                RefreshToken = refreshToken
            };

            return Ok(token);
        }

        [HttpPost("refresh")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> Refresh_1_0([FromBody] TokenDto token, ApiVersion apiVersion)
        {
            var principal = GetPrincipalFromExpiredToken(token.Token);
            var user = await _userManager.FindByNameAsync(principal.Identity.Name);

            if(user == null)
            {
                return Unauthorized();
            }

            if(user.RefreshToken != token.RefreshToken || DateTime.UtcNow > user.RefreshTokenExpiry)
            {
                throw new SecurityTokenException("Invalid refresh token!");
            }

            var refreshToken = await _jwtService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddMinutes(_jwtSettings.RefreshTokenExpiryMinutes);
            user.SecurityStamp = Guid.NewGuid().ToString();

            var identityResult = await _userManager.UpdateAsync(user);

            if(!identityResult.Succeeded)
            {
                return Unauthorized();
            }

            var newToken = new TokenDto(){
                Token = await _jwtService.CreateJwt(principal.Identity as ClaimsIdentity),
                RefreshToken = refreshToken
            };

            return Ok(newToken);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentityAsync(CredentialsDto credentials)
        {
            ApplicationUser applicationUser = await _userManager.FindByNameAsync(credentials.Username);

            if (applicationUser == null)
            {
                return null;
            }

            if (await _userManager.CheckPasswordAsync(applicationUser, credentials.Password))
            {
                return await GetClaimsIdentity(applicationUser);
            }

            return null;
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(ApplicationUser applicationUser)
        {
            var claims = new List<Claim>();

            var roles = await _userManager.GetRolesAsync(applicationUser);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return new ClaimsIdentity(new GenericIdentity(applicationUser.UserName), claims);
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.HmacSecretKey));
            
            var tokenValidationParams = new TokenValidationParameters
                {
                    //* Move these into Settings
                    ClockSkew = TimeSpan.FromMinutes(_jwtSettings.ClockSkewMinutes),
                    IssuerSigningKey = issuerSigningKey,
                    RequireSignedTokens = _jwtSettings.RequireSignedTokens,
                    RequireExpirationTime = _jwtSettings.RequireExpirationTime,
                    ValidateLifetime = false, // We know the token is expired
                    ValidateAudience = _jwtSettings.ValidateAudience,
                    ValidAudience = _jwtSettings.Audience,
                    ValidateIssuer = _jwtSettings.ValidateIssuer,
                    ValidIssuer = _jwtSettings.Issuer
                };

            var tokenHandler = new JwtSecurityTokenHandler();
            
            var principal = tokenHandler.ValidateToken(token, tokenValidationParams, out SecurityToken validatedToken);

            var jwtToken = validatedToken as JwtSecurityToken;

            if(jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token!");
            }

            return principal;
        }
    }
}