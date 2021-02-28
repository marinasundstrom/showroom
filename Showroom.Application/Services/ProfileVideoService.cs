using System;
using System.IO;
using System.Threading.Tasks;

using Showroom.Application.Common.Interfaces;
using Showroom.Domain.Entities;
using Showroom.Domain.Exceptions;

namespace Showroom.Application.Services
{
    public class ProfileVideoService : IProfileVideoService
    {
        private readonly IApplicationDbContext applicationDbContext;
        private readonly IVideoUploader videoUploader;

        public ProfileVideoService(IApplicationDbContext applicationDbContext, IVideoUploader videoUploader)
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
