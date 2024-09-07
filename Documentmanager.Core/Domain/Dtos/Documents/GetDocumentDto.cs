using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documentmanager.Core.Domain.Dtos.Documents
{
    public class GetDocumentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StoragePath { get; set; }
        public int UserId { get; set; }
        public int? OrganizationId { get; set; }
    }
}
