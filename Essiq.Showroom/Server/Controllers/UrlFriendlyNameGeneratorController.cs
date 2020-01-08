using System;
using System.Threading.Tasks;

using Essiq.Showroom.Server.Data;
using Essiq.Showroom.Server.Services;

using Microsoft.AspNetCore.Mvc;

namespace Essiq.Showroom.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlFriendlyNameGeneratorController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UrlFriendlyNameGenerator urlFriendlyNameGenerator;

        public UrlFriendlyNameGeneratorController(
            ApplicationDbContext context,
            UrlFriendlyNameGenerator urlFriendlyNameGenerator)
        {
            this.context = context;
            this.urlFriendlyNameGenerator = urlFriendlyNameGenerator;
        }

        [HttpGet("GenerateUrlFriendlyNameForProfile")]
        public async Task<string> GenerateUrlFriendlyNameForProfile(Guid userProfileId)
        {
            var up = await context.UserProfiles.FindAsync(userProfileId);
            if (up == null)
            {
                throw new Exception();
            }
            return await urlFriendlyNameGenerator.GenerateUrlFriendlyNameForProfileAsync(up);
        }

        [HttpGet("CheckUrlFriendlyNameAvailableForUserProfile")]
        public async Task<bool> CheckUrlFriendlyNameAvailableForUserProfile(string shortName)
        {
            return await urlFriendlyNameGenerator.CheckUrlFriendlyNameForUserProfile(shortName);
        }
    }
}
