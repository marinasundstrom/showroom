using System.IO;
using System.Threading.Tasks;

namespace Essiq.Showroom.Server.Services
{
    public interface IImageUploader
    {
        Task DeleteImageAsync(string id);
        Task<string> UploadImageAsync(string id, Stream stream);
    }
}