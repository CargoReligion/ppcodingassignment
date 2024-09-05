using Documentmanager.Core.Domain.Dtos;
using Documentmanager.Core.Domain.Models.Organizations;
using Documentmanager.Core.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documentmanager.Core.Domain.Services.Organizations
{
    public class OrganizationService
    {
        private readonly IOrganizationRepository _repository;

        public OrganizationService(IOrganizationRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateOrganization(OrganizationDto dto, int userId)
        {
            var existingOrganization = await _repository.GetByName(dto.Name);
            //TODO: Check if already exists
            var organization = new Organization(dto.Name, userId);
            return await _repository.Create(organization);
        }
    }
}
