using System.Security.Claims;

using Showroom.Server.Models;

namespace Showroom.Server.Services
{
    public interface IJwtTokenService
    {
        string BuildToken(User user, string role);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
