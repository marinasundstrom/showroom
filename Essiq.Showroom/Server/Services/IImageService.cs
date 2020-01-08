using System.IO;
using System.Threading.Tasks;

namespace Essiq.Showroom.Server.Services
{
    public interface IImageService
    {
        Task<Stream> GetImageByName(string name);
    }
}