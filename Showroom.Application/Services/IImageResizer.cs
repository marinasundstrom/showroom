using System.IO;
using System.Threading.Tasks;

namespace Showroom.Application.Services
{
    public interface IImageResizer
    {
        Task<Stream> ResizeImage(Stream inputStream, int? destinationWidth = null, int? destinationHeight = null);
    }
}
