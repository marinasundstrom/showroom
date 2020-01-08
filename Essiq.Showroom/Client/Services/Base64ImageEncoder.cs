using System;
using System.IO;

namespace Essiq.Showroom.Client.Services
{
    public static class Base64ImageEncoder
    {
        public static string EncodeImage(MemoryStream stream, string type)
        {
            var bytes = stream.ToArray();
            var base64String = Convert.ToBase64String(bytes);

            return $"data:{type};base64,{base64String}";
        }
    }
}
