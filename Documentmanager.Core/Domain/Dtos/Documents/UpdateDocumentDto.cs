using Microsoft.AspNetCore.Http;

namespace Documentmanager.Core.Domain.Dtos.Documents
{
    public class UpdateDocumentDto
    {
        public int Id { get; set; }
        public IFormFile File { get; set; }
        public string Name { get; set; }
    }
}
