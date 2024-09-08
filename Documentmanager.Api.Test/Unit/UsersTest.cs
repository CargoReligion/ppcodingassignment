using AutoMapper;
using Documentmanager.Core.Domain.Dtos.Users;
using Documentmanager.Core.Domain.Models.Users;
using Documentmanager.Core.Domain.Repositories.Interfaces;
using Documentmanager.Core.Domain.Services.Users;
using FluentAssertions;
using NSubstitute;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Documentmanager.Api.Test.Unit
{
    public class UsersTest
    {
        [Fact(DisplayName = "Should create Users")]
        public async Task ShouldCreateUsers()
        {
            var dto = new CreateUserDto
            {
                Email = "something@email.com"
            };
            var userId = 123;
            var returnedId = 234;
            var userRepository = Substitute.For<IUserRepository>();
            userRepository
                .Create(Arg.Is<User>(
                        o => o.Email == dto.Email && o.CreatedBy == userId
                    ))
                .Returns(returnedId);
            var mapper = Substitute.For<IMapper>();
            var userService = new UserService(userRepository, mapper);

            var createResult = await userService.CreateUser(dto, userId);

            createResult.SuccessData.Should().Be(returnedId);
        }

        [Fact(DisplayName = "Should not create User if email already exists")]
        public async Task ShouldNotCreateUserSameEmail()
        {
            var dto = new CreateUserDto
            {
                Email = "something@email.com"
            };
            var userId = 123;
            var userRepository = Substitute.For<IUserRepository>();
            userRepository
                .GetByEmail(dto.Email)
                .Returns(new User("something@email.com", userId));
            var mapper = Substitute.For<IMapper>();
            var userService = new UserService(userRepository, mapper);

            var createResult = await userService.CreateUser(dto, userId);

            createResult.IsSuccess.Should().BeFalse();
            createResult.Errors.First().Should().Be($"User with email {dto.Email} already exists.");
        }

        // In real world, would write way more tests especially integration tests.
    }
}