using System;
using System.IO;

namespace Showroom.Client.Services
{
    public static class Base64ImageEncoder
    {
        public static string EncodeImage(Stream stream, string type)
        {
            byte[] bytes = null;
            if (stream is MemoryStream memoryStream)
            {
                bytes = memoryStream.ToArray();
            }
            else
            {
                using (memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    bytes = memoryStream.ToArray();
                }
            }
            var base64String = Convert.ToBase64String(bytes);

            return $"data:{type};base64,{base64String}";
        }
    }
}
