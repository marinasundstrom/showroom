﻿using System;
using System.IO;
using System.Threading.Tasks;

namespace Showroom.Application.Services
{
    public interface IProfileVideoService
    {
        Task DeleteProfileVideoAsync(Guid userProfileId);
        Task<string> UploadProfileVideoAsync(Guid userProfileId, string fileName, Stream stream);
    }
}
