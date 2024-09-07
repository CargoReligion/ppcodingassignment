using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documentmanager.Core.Domain.Dtos.Documents
{
    public class CreateDocumentDto
    {
        public IFormFile File { get; set; }
        public string Name { get; set; }
    }
}
