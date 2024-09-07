using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documentmanager.Core.Domain.Services.Common
{
    public interface IFileService
    {
        public Task<string> UploadFile(IFormFile file);
        public Task DeleteFile(string storagePath);
    }
}
