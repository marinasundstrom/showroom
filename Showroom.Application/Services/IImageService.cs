using System.IO;
using System.Threading.Tasks;

namespace Showroom.Application.Services
{
    public interface IImageService
    {
        Task<Stream> GetImageByName(string name);
    }
}