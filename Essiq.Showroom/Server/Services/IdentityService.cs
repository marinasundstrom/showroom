using System.Security.Claims;
using System.Threading.Tasks;

using Essiq.Showroom.Server.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Essiq.Showroom.Server.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public IdentityService(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<User> GetUserAsync()
        {
            var claimsIdentity = (ClaimsIdentity)httpContextAccessor.HttpContext.User.Identity;
            var email = claimsIdentity.FindFirst(ClaimTypes.Email).Value;
            return await userManager.FindByEmailAsync(email);
        }
    }
}
