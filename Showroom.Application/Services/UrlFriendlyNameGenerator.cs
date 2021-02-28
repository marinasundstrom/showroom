﻿using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Showroom.Application.Common.Interfaces;
using Showroom.Domain.Entities;

namespace Showroom.Application.Services
{
    public sealed class UrlFriendlyNameGenerator
    {
        private readonly IApplicationDbContext applicationDbContext;

        public UrlFriendlyNameGenerator(IApplicationDbContext applicationDbContext)
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
