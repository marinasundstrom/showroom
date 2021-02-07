using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Showroom.Server.Models;
using Showroom.Server.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Showroom.Server.Controllers
{

    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IJwtTokenService _tokenService;
        private readonly UserManager<User> _userManager;

        public TokenController(
            IJwtTokenService tokenService,
            UserManager<User> userManager)
        {
            _tokenService = tokenService;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("Authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<TokenResult>> Authenticate([FromForm] string email, [FromForm] string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            User user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound();
            }

            //if (!user.EmailConfirmed)
            //{
            //    var problem = new ProblemDetails
            //    {
            //        Title = "Email has not been confirmed",
            //        Detail = "Email has not been confirmed",
            //    };

            //    return BadRequest(problem);
            //}

            bool validCredentials = await _userManager.CheckPasswordAsync(user, password);

            if (!validCredentials)
            {
                var problem = new ProblemDetails
                {
                    Title = "Username or password is incorrect",
                    Detail = "Username or password is incorrect",
                };

                return BadRequest(problem);
            }

            var role = (await _userManager.GetRolesAsync(user)).First();

            string newJwtToken = GenerateToken(user, role);
            string newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return Ok(new TokenResult
            {
                Token = newJwtToken,
                RefreshToken = newRefreshToken
            });
        }

        [HttpPost]
        [Route("Refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<TokenResult>> Refresh([FromForm] string token, [FromForm] string refreshToken)
        {
            ClaimsPrincipal principal = _tokenService.GetPrincipalFromExpiredToken(token);
            string email = GetEmailFromClaimsPrincipal(principal);

            User user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound();
            }

            string savedRefreshToken = user.RefreshToken;

            if (savedRefreshToken != refreshToken)
            {
                throw new SecurityTokenException("Invalid refresh token");
            }

            var role = (await _userManager.GetRolesAsync(user)).First();

            string newJwtToken = GenerateToken(user, role);
            string newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return Ok(new TokenResult
            {
                Token = newJwtToken,
                RefreshToken = newRefreshToken
            });
        }

        private static string GetEmailFromClaimsPrincipal(ClaimsPrincipal principal)
        {
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)principal.Identity;
            return claimsIdentity.FindFirst(ClaimTypes.Email).Value;
        }

        private string GenerateToken(User user, string role)
        {
            return _tokenService.BuildToken(user, role);
        }

        private string GenerateRefreshToken()
        {
            return _tokenService.GenerateRefreshToken();
        }
    }
}
