using AutoMapper;
using Documentmanager.Core.Domain.Dtos.Documents;
using Documentmanager.Core.Domain.Dtos.Organizations;
using Documentmanager.Core.Domain.Dtos.Users;
using Documentmanager.Core.Domain.Models.Document;
using Documentmanager.Core.Domain.Models.Organizations;
using Documentmanager.Core.Domain.Models.Users;

namespace Documentmanager.Core.Domain.Mapping
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Organization, GetOrganizationDto>();
            CreateMap<User, GetUserDto>();
            CreateMap<Document, GetDocumentDto>();

        }
    }
}
