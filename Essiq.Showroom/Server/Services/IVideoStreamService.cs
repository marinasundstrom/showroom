using System.IO;
using System.Threading.Tasks;

namespace Essiq.Showroom.Server.Services
{
    public interface IVideoStreamService
    {
        Task<Stream> GetVideoByName(string name);
    }
}