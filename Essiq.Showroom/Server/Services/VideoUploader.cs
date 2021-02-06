using System.IO;
using System.Threading.Tasks;

namespace Essiq.Showroom.Server.Services
{
    public sealed class VideoUploader : IVideoUploader
    {
        private readonly string videoFolderPath;

        public VideoUploader()
        {
            videoFolderPath = Path.Combine("../../Assets", "videos");

            if (!Directory.Exists(videoFolderPath))
            {
                Directory.CreateDirectory(videoFolderPath);
            }
        }

        public async Task<string> UploadVideoAsync(string id, Stream stream)
        {
            var videoFilePath = Path.Combine(videoFolderPath, id);
            using (var fileStream = File.OpenWrite(videoFilePath))
            {
                await stream.CopyToAsync(fileStream);
            }
            return ToUrl(id);

        }

        private string ToUrl(string id)
        {
            return $"/videos/{id}";
        }

        public async Task DeleteVideoAsync(string id)
        {
            await Task.Run(() =>
            {
                var videoFilePath = Path.Combine(videoFolderPath, id);
                File.Delete(videoFilePath);
            });
        }
    }
}
