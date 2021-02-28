using System.IO;
using System.Threading.Tasks;

namespace Showroom.Application.Services
{
    public interface IImageUploader
    {
        Task DeleteImageAsync(string id);
        Task<string> UploadImageAsync(string id, Stream stream);
    }
}