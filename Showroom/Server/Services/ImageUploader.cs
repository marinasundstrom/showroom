using System.IO;
using System.Threading.Tasks;

namespace Showroom.Server.Services
{
    public sealed class ImageUploader : IImageUploader
    {
        private readonly string imageFolderPath;

        public ImageUploader()
        {
            imageFolderPath = Path.Combine("../../Assets", "images");

            if (!Directory.Exists(imageFolderPath))
            {
                Directory.CreateDirectory(imageFolderPath);
            }
        }

        public async Task<string> UploadImageAsync(string id, Stream stream)
        {
            var imageFilePath = Path.Combine(imageFolderPath, id);
            using (var fileStream = File.OpenWrite(imageFilePath))
            {
                await stream.CopyToAsync(fileStream);
            }
            return ToUrl(id);

        }

        private string ToUrl(string id)
        {
            return $"/images/{id}";
        }

        public async Task DeleteImageAsync(string id)
        {
            await Task.Run(() =>
            {
                var imageFilePath = Path.Combine(imageFolderPath, id);
                File.Delete(imageFilePath);
            });
        }
    }
}
