using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace Essiq.Showroom.Server.Services
{
    public class ImageResizer : IImageResizer
    {
        public async Task<Stream> ResizeImage(Stream inputStream, int? destinationWidth = null, int? destinationHeight = null)
        {
            return await Task.Run(() =>
            {
                using (var temp = new Bitmap(inputStream))
                {
                    var image = new Bitmap(temp);
                    var destinationImage = ResizeImage(image, destinationWidth ?? image.Width, destinationHeight ?? image.Height, true);
                    var outputStream = new MemoryStream();
                    destinationImage.Save(outputStream, ImageFormat.Jpeg);
                    outputStream.Seek(0, SeekOrigin.Begin);
                    return outputStream;
                }
            });
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height, bool enlarge = false)
        {
            var newSize = ResizeKeepAspect(new Size(image.Width, image.Height), width, height, enlarge);

            var destRect = new Rectangle(0, 0, newSize.Width, newSize.Height);
            var destImage = new Bitmap(newSize.Width, newSize.Height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);


            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static Size ResizeKeepAspect(Size src, int maxWidth, int maxHeight, bool enlarge = false)
        {
            maxWidth = enlarge ? maxWidth : Math.Min(maxWidth, src.Width);
            maxHeight = enlarge ? maxHeight : Math.Min(maxHeight, src.Height);

            decimal rnd = Math.Min(maxWidth / (decimal)src.Width, maxHeight / (decimal)src.Height);
            return new Size((int)Math.Round(src.Width * rnd), (int)Math.Round(src.Height * rnd));
        }
    }
}
