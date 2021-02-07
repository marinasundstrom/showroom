using System.Threading.Tasks;

using Showroom.Server.Data;
using Showroom.Server.Models;

using Microsoft.EntityFrameworkCore;

namespace Showroom.Server.Services
{
    public sealed class UrlFriendlyNameGenerator
    {
        private readonly ApplicationDbContext applicationDbContext;

        public UrlFriendlyNameGenerator(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<string> GenerateUrlFriendlyNameForProfileAsync(UserProfile userProfile)
        {
            var proposedName = string.Empty;

            if (!string.IsNullOrEmpty(userProfile.DisplayName))
            {
                proposedName = userProfile.DisplayName;
            }
            else
            {
                proposedName = userProfile.FirstName;

                if (!string.IsNullOrEmpty(userProfile.MiddleName))
                {
                    proposedName += $"-{userProfile.MiddleName}";
                }

                proposedName += $"-{userProfile.LastName}";
            }

            proposedName = proposedName.ToLower();

            var count = await applicationDbContext.UserProfiles.CountAsync(up => up.Slug == proposedName);

            if (count > 0)
            {
                proposedName = $"{proposedName}{count}";
            }

            return proposedName;
        }

        public async Task<bool> CheckUrlFriendlyNameForUserProfile(string name)
        {
            return !await applicationDbContext.UserProfiles.AnyAsync(up => up.Slug == name);
        }
    }
}
