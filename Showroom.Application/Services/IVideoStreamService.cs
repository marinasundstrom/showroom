using System.IO;
using System.Threading.Tasks;

namespace Showroom.Application.Services
{
    public interface IVideoStreamService
    {
        Task<Stream> GetVideoByName(string name);
    }
}