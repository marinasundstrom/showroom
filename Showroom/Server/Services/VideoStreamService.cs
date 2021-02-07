using System;
using System.IO;
using System.Threading.Tasks;

namespace Showroom.Server.Services
{
    public sealed class VideoStreamService : IVideoStreamService
    {
        private string videoFolderPath;

        public VideoStreamService()
        {
            videoFolderPath = Path.Combine("../../Assets", "Videos");
        }

        public async Task<Stream> GetVideoByName(string name)
        {
            return await Task.Run(() =>
            {
                var videoFilePath = Path.Combine(videoFolderPath, name);
                return File.OpenRead(videoFilePath);
            });
        }
    }
}
