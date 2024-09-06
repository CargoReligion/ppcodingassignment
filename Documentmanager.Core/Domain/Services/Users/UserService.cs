using AutoMapper;
using Documentmanager.Core.Domain.Dtos.Users;
using Documentmanager.Core.Domain.Models.Common;
using Documentmanager.Core.Domain.Models.Users;
using Documentmanager.Core.Domain.Repositories.Interfaces;

namespace Documentmanager.Core.Domain.Services.Users
{
    public class UserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<GetUserDto>> FindById(int id)
        {
            var result = new Result<GetUserDto>();
            var user = await _repository.GetById(id);
            if (user == null)
            {
                result.AddError($"User with id {id} not found.");
                return result;
            }
            var dto = _mapper.Map<GetUserDto>(user);
            result.AddSuccessData(dto);
            return result;
        }

        public async Task<Result<IEnumerable<GetUserDto>>> GetAll()
        {
            var result = new Result<IEnumerable<GetUserDto>>();
            var users = await _repository.GetAll();
            var dtos = _mapper.Map<IEnumerable<GetUserDto>>(users);
            result.AddSuccessData(dtos);
            return result;
        }

        public async Task<Result<int>> CreateUser(CreateUserDto dto, int userId)
        {
            var result = new Result<int>();
            var existingUser = await _repository.GetByEmail(dto.Email);
            if (existingUser != null)
            {
                result.AddError($"User with name {dto.Email} already exists.");
            }
            else
            {
                var user = new User(dto.Email, userId);
                var createdId = await _repository.Create(user);
                result.AddSuccessData(createdId);
            }
            return result;
        }

        public async Task<Result<int>> UpdateUser(UpdateUserDto dto, int userId)
        {
            var result = new Result<int>();
            var userWithSameEmail = await _repository.GetByEmail(dto.Email);
            if (userWithSameEmail != null)
            {
                result.AddError($"User with name {dto.Email} already exists.");
                return result;
            }
            else
            {
                var existingUser = await _repository.GetById(dto.Id);
                if (existingUser == null)
                {
                    result.AddError($"User with id {dto.Id} not found.");
                    return result;
                }
                existingUser.UpdateEmail(dto);
                var updatedId = await _repository.Update(existingUser);
                result.AddSuccessData(updatedId);
            }
            return result;
        }

        public async Task<Result<int>> DeleteUser(int id)
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
