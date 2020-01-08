using System;
using System.IO;
using System.Threading.Tasks;

using Essiq.Showroom.Server.Controllers;
using Essiq.Showroom.Server.Data;
using Essiq.Showroom.Server.Models;

namespace Essiq.Showroom.Server.Services
{
    public class ProfileVideoService : IProfileVideoService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IVideoUploader videoUploader;

        public ProfileVideoService(ApplicationDbContext applicationDbContext, IVideoUploader videoUploader)
        {
            this.applicationDbContext = applicationDbContext;
            this.videoUploader = videoUploader;
        }

        public async Task<string> UploadProfileVideoAsync(Guid userProfileId, string fileName, Stream stream)
        {
            var userProfile = await applicationDbContext.UserProfiles.FindAsync(userProfileId);
            if (userProfile == null)
            {
                throw new NotFoundException(nameof(UserProfile), userProfileId);
            }
            var imageUrl = await videoUploader.UploadVideoAsync(fileName, stream);

            userProfile.ProfileVideo = imageUrl;
            await this.applicationDbContext.SaveChangesAsync();

            return imageUrl;
        }

        public async Task DeleteProfileVideoAsync(Guid userProfileId)
        {
            var userProfile = await applicationDbContext.UserProfiles.FindAsync(userProfileId);
            if (userProfile == null)
            {
                throw new NotFoundException(nameof(UserProfile), userProfileId);
            }

            userProfile.ProfileVideo = null;
            await applicationDbContext.SaveChangesAsync();
        }
    }
}
