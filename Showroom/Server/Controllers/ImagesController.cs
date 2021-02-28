using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Showroom.Application.Services;

namespace Showroom.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private IImageService _imageService;
        private readonly IImageResizer imageResizer;

        public ImagesController(IImageService imageService, IImageResizer imageResizer)
        {
            _imageService = imageService;
            this.imageResizer = imageResizer;
        }

        [HttpGet("{name}")]
        [ResponseCache(Duration = 1, VaryByQueryKeys = new[] { "*" })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetImage(string name,
            [FromQuery] int? width = null, [FromQuery] int? height = null)
        {
            try
            {
                var stream = await _imageService.GetImageByName(name);

                if (HttpContext.Request.Query.Count > 0)
                {
                    stream = await imageResizer.ResizeImage(stream, width, height);
                }

                return File(stream, "image/jpg");
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
