using System;
using System.IO;
using System.Threading.Tasks;

using Essiq.Showroom.Server.Controllers;
using Essiq.Showroom.Server.Data;
using Essiq.Showroom.Server.Models;

namespace Essiq.Showroom.Server.Services
{
    public class ProfileImageService : IProfileImageService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IImageUploader imageUploader;

        public ProfileImageService(ApplicationDbContext applicationDbContext, IImageUploader imageUploader)
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
