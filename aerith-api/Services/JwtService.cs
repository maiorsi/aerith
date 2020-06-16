using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Aerith.Api.Interfaces;
using Aerith.Api.Settings;
using Aerith.Common.Models.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Aerith.Api.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _settings;
        private readonly SigningCredentials _signingCredentials;

        public JwtService(IOptions<JwtSettings> settings)
        {
            _settings = settings.Value ?? throw new ArgumentNullException(nameof(settings));

            var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.HmacSecretKey));

            _signingCredentials = new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha512);
        }

        public Task<string> CreateJwt(ClaimsIdentity claimsIdentity)
        {
            var nowUtc = DateTime.UtcNow;
            var expires = nowUtc.AddMinutes(_settings.TokenExpiryMinutes);
            var centuryBegin = new DateTime(1970, 1, 1);
            var exp = (long)(new TimeSpan(expires.Ticks - centuryBegin.Ticks).TotalSeconds);
            var now = (long)(new TimeSpan(nowUtc.Ticks - centuryBegin.Ticks).TotalSeconds);
            var payload = new JwtPayload
            {
                {"sub", claimsIdentity.Name},
                {"unique_name", claimsIdentity.Name},
                {"iss", _settings.Issuer},
                {"aud", _settings.Audience},
                {"iat", now},
                {"nbf", now},
                {"exp", exp},
                {"jti", Guid.NewGuid().ToString("N")}
            };

            payload.AddClaims(claimsIdentity.Claims.Where(_ => _.Type.Equals(ClaimTypes.Role)).ToList());
            
            var jwtHeader = new JwtHeader(_signingCredentials);

            var jwt = new JwtSecurityToken(jwtHeader, payload);

            var jwtHandler = new JwtSecurityTokenHandler();
           
            return Task.FromResult(jwtHandler.WriteToken(jwt));
        }
    }
}