using Documentmanager.Core.Domain.Dtos.OrganizationUser;
using Documentmanager.Core.Domain.Models.Common;
using Documentmanager.Core.Domain.Repositories.Interfaces;
using Documentmanager.Core.Domain.Services.Organizations;
using Documentmanager.Core.Domain.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documentmanager.Core.Domain.Services.OrganizationUser
{
    public class OrganizationUserService
    {
        private readonly IOrganizationRepository _orgRepository;
        private readonly IUserRepository _userRepository;

        public OrganizationUserService(IOrganizationRepository orgRepository, IUserRepository userRepository)
        {
            _orgRepository = orgRepository;
            _userRepository = userRepository;
        }

        public async Task<Result<int>> UpdateUserOrganization(UpdateOrganizationUserDto dto, int userId)
        {
            var result = new Result<int>();
            var organizationToFind = await _orgRepository.GetById(dto.OrganizationId);
            if (organizationToFind == null)
            {
                result.AddError($"Organization with Id {dto.OrganizationId} does not exist.");
                return result;
            }
            var user = await _userRepository.GetById(dto.UserId);
            if (user == null)
            {
                result.AddError($"User with Id {dto.UserId} does not exist.");
                return result;
            }
            user.UpdateUserOrganization(dto, userId);
            result.AddSuccessData(await _userRepository.Update(user));
            return result;
        }
    }
}
