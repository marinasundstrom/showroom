﻿using System.IO;
using System.Threading.Tasks;

namespace Showroom.Application.Services
{
    public sealed class ImageService : IImageService
    {
        private string imageFolderPath;

        public ImageService()
        {
            imageFolderPath = Path.GetFullPath(Path.Combine("../../Assets", "images"));
        }

        public async Task<Stream> GetImageByName(string name)
        {
            return await Task.Run(async () =>
            {
                var imageFilePath = Path.Combine(imageFolderPath, name);
                using (var stream = File.OpenRead(imageFilePath))
                {
                    var memoryStream = new MemoryStream();
                    await stream.CopyToAsync(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    return memoryStream;
                }
            });
        }
    }
}
