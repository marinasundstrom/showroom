using System.Threading.Tasks;

using Essiq.Showroom.Server.Services;

using Microsoft.AspNetCore.Mvc;

namespace Essiq.Showroom.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VideosController : ControllerBase
    {
        private IVideoStreamService _streamingService;

        public VideosController(IVideoStreamService streamingService)
        {
            _streamingService = streamingService;
        }

        [HttpGet("{name}")]
        public async Task<FileStreamResult> Get(string name)
        {
            var stream = await _streamingService.GetVideoByName(name);
            return new FileStreamResult(stream, "video/mp4");
        }
    }
}
