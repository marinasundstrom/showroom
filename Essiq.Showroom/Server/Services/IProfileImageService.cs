using System;
using System.IO;
using System.Threading.Tasks;

namespace Essiq.Showroom.Server.Services
{
    public interface IProfileImageService
    {
        Task DeleteProfileImageAsync(Guid userProfileId);
        Task<string> UploadProfileImageAsync(Guid userProfileId, string fileName, Stream stream);
    }
}