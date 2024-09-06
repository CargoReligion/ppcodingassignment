using AutoMapper;
using Documentmanager.Core.Domain.Dtos;
using Documentmanager.Core.Domain.Models.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documentmanager.Core.Domain.Mapping
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Organization, GetOrganizationDto>();
        }
    }
}
