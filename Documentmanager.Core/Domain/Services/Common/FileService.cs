using Microsoft.AspNetCore.Http;

namespace Documentmanager.Core.Domain.Services.Common
{
    public class FileService : IFileService
    {
        public Task<string> UploadFile(IFormFile file)
        {
            // In real life, would upload to some blob storage like Azure blob storage
            var path =  $"https://blob.com/{Guid.NewGuid()}";
            return Task.FromResult(path);
        }

        public Task DeleteFile(string storagePath)
        {
            // Go to storage and remove
            return Task.CompletedTask;
        }
    }
}
