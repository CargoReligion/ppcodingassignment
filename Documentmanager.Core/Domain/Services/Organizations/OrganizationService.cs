using AutoMapper;
using Documentmanager.Core.Domain.Dtos;
using Documentmanager.Core.Domain.Dtos.Organizations;
using Documentmanager.Core.Domain.Models.Common;
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
        private readonly IMapper _mapper;

        public OrganizationService(IOrganizationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<GetOrganizationDto>> FindById(int id)
        {
            var result = new Result<GetOrganizationDto>();
            var organization = await _repository.GetById(id);
            if (organization == null)
            {
                result.AddError($"Organization with id {id} not found.");
                return result;
            }
            var dto = _mapper.Map<GetOrganizationDto>(organization);
            result.AddSuccessData(dto);
            return result;
        }

        public async Task<Result<IEnumerable<GetOrganizationDto>>> GetAll()
        {
            var result = new Result<IEnumerable<GetOrganizationDto>>();
            var organizations = await _repository.GetAll();
            var dtos = _mapper.Map<IEnumerable<GetOrganizationDto>>(organizations);
            result.AddSuccessData(dtos);
            return result;
        }

        public async Task<Result<int>> CreateOrganization(CreateOrganizationDto dto, int userId)
        {
            var result = new Result<int>();
            var existingOrganization = await _repository.GetByName(dto.Name);
            if (existingOrganization != null)
            {
                result.AddError($"Organization with name {dto.Name} already exists.");
            }
            else
            {
                var organization = new Organization(dto.Name, userId);
                var createdId = await _repository.Create(organization);
                result.AddSuccessData(createdId);
            }
            return result;
        }

        public async Task<Result<int>> UpdateOrganization(UpdateOrganizationDto dto, int userId)
        {
            var result = new Result<int>();
            var organizationWithSameName = await _repository.GetByName(dto.Name);
            if (organizationWithSameName != null)
            {
                result.AddError($"Organization with name {dto.Name} already exists.");
                return result;
            }
            else
            {
                var existingOrganization = await _repository.GetById(dto.Id);
                if (existingOrganization == null)
                {
                    result.AddError($"Organization with id {dto.Id} not found.");
                    return result;
                }
                existingOrganization.UpdateName(dto);
                var updatedId = await _repository.Update(existingOrganization);
                result.AddSuccessData(updatedId);
            }
            return result;
        }

        public async Task<Result<int>> DeleteOrganization(int id, int userId)
        {
            var result = new Result<int>();
            var existingOrganization = await _repository.GetById(id);
            if (existingOrganization == null)
            {
                result.AddError($"Organization with id {id} not found.");
                return result;
            }
            await _repository.Delete(existingOrganization);
            result.AddSuccessData(existingOrganization.Id);
            return result;
        }
    }
}
