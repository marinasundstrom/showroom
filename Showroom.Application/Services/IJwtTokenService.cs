using System.Security.Claims;

using Showroom.Domain.Entities;

namespace Showroom.Application.Services
{
    public interface IJwtTokenService
    {
        string BuildToken(User user, string role);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
