using System.IO;
using System.Threading.Tasks;

namespace Showroom.Application.Services
{
    public interface IVideoUploader
    {
        Task DeleteVideoAsync(string id);
        Task<string> UploadVideoAsync(string id, Stream stream);
    }
}