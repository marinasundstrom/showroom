using System.Security.Claims;

using Essiq.Showroom.Server.Models;

namespace Essiq.Showroom.Server.Services
{
    public interface IJwtTokenService
    {
        string BuildToken(User user, string role);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
