using System.IO;
using System.Threading.Tasks;

namespace Essiq.Showroom.Server.Services
{
    public interface IImageResizer
    {
        Task<Stream> ResizeImage(Stream inputStream, int? destinationWidth = null, int? destinationHeight = null);
    }
}
