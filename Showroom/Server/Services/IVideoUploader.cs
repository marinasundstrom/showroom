using System.IO;
using System.Threading.Tasks;

namespace Showroom.Server.Services
{
    public interface IVideoUploader
    {
        Task DeleteVideoAsync(string id);
        Task<string> UploadVideoAsync(string id, Stream stream);
    }
}