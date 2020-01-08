using System.IO;
using System.Threading.Tasks;

namespace Essiq.Showroom.Server.Services
{
    public sealed class VideoStreamService : IVideoStreamService
    {
        private string videoFolderPath;

        public VideoStreamService()
        {
            videoFolderPath = Path.Combine("C:/temp", "videos");
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
