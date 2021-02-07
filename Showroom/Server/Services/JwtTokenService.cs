using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using Showroom.Server.Configuration;
using Showroom.Server.Models;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Showroom.Server.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _config;
        private readonly JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        public JwtTokenService(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Builds the token used for authentication
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public string BuildToken(User user, string role)
        {
            JwtConfiguration jwtConfig = _config.GetSection("Jwt").Get<JwtConfiguration>();

            // Create a claim based on the users emai. You can add more claims like ID's and any other info
            Claim[] claims = new[] {
                  new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                  new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                  new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                  new Claim(JwtRegisteredClaimNames.Email, user.Email),
                  new Claim(ClaimTypes.Role, role)
            };

            // Creates a key from our private key that will be used in the security algorithm next
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key));

            // Credentials that are encrypted which can only be created by our server using the private key
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // this is the actual token that will be issued to the user
            JwtSecurityToken token = new JwtSecurityToken(jwtConfig.Issuer,
                jwtConfig.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(double.Parse(jwtConfig.ExpireTime)),
                signingCredentials: creds);

            // return the token in the correct format using JwtSecurityTokenHandler
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            byte[] randomNumber = new byte[32];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            JwtConfiguration jwtConfig = _config.GetSection("Jwt").Get<JwtConfiguration>();

            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtConfig.Issuer,
                ValidAudience = jwtConfig.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key)),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };

            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
    }
}
