using System.Security.Claims;
using System.Threading.Tasks;

namespace Aerith.Api.Interfaces
{
    public interface IJwtService
    {
        Task<string> CreateJwt(ClaimsIdentity claimsIdentity);
        Task<string> GenerateRefreshToken();
    }
}