using System;
using System.IO;
using System.Threading.Tasks;

using Showroom.Application.Common.Interfaces;
using Showroom.Domain.Entities;
using Showroom.Domain.Exceptions;

namespace Showroom.Application.Services
{
    public class ProfileImageService : IProfileImageService
    {
        private readonly IApplicationDbContext applicationDbContext;
        private readonly IImageUploader imageUploader;

        public ProfileImageService(IApplicationDbContext applicationDbContext, IImageUploader imageUploader)
        {
            this.applicationDbContext = applicationDbContext;
            this.imageUploader = imageUploader;
        }

        public async Task<string> UploadProfileImageAsync(Guid userProfileId, string fileName, Stream stream)
        {
            var userProfile = await applicationDbContext.UserProfiles.FindAsync(userProfileId);
            if (userProfile == null)
            {
                throw new NotFoundException(nameof(UserProfile), userProfileId);
            }
            var imageUrl = await imageUploader.UploadImageAsync(fileName, stream);

            userProfile.ProfileImage = imageUrl;
            await this.applicationDbContext.SaveChangesAsync();

            return imageUrl;
        }

        public async Task DeleteProfileImageAsync(Guid userProfileId)
        {
            var userProfile = await applicationDbContext.UserProfiles.FindAsync(userProfileId);
            if (userProfile == null)
            {
                throw new NotFoundException(nameof(UserProfile), userProfileId);
            }

            userProfile.ProfileImage = null;
            await applicationDbContext.SaveChangesAsync();
        }
    }
}
