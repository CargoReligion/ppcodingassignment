using AutoMapper;
using Documentmanager.Core.Domain.Dtos.Organizations;
using Documentmanager.Core.Domain.Models.Organizations;
using Documentmanager.Core.Domain.Repositories.Interfaces;
using Documentmanager.Core.Domain.Services.Organizations;
using FluentAssertions;
using NSubstitute;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Documentmanager.Api.Test.Unit
{
    public class OrganizationsTest
    {
        [Fact(DisplayName = "Should create Organization")]
        public async Task ShouldCreateOrganization()
        {
            var dto = new CreateOrganizationDto
            {
                Name = "Org1"
            };
            var userId = 123;
            var returnedId = 234;
            var organizationRepository = Substitute.For<IOrganizationRepository>();
            organizationRepository
                .Create(Arg.Is<Organization>(
                        o => o.Name == dto.Name && o.CreatedBy == userId
                    ))
                .Returns(returnedId);
            var mapper = Substitute.For<IMapper>();
            var organizationService = new OrganizationService(organizationRepository, mapper);

            var createResult = await organizationService.CreateOrganization(dto, userId);

            createResult.SuccessData.Should().Be(returnedId);
        }

        [Fact(DisplayName = "Should not create Organization if name already exists")]
        public async Task ShouldNotCreateOrganizationSameName()
        {
            var dto = new CreateOrganizationDto
            {
                Name = "Org1"
            };
            var userId = 123;
            var organizationRepository = Substitute.For<IOrganizationRepository>();
            organizationRepository
                .GetByName(dto.Name)
                .Returns(new Organization("Org1", userId));
            var mapper = Substitute.For<IMapper>();
            var organizationService = new OrganizationService(organizationRepository, mapper);

            var createResult = await organizationService.CreateOrganization(dto, userId);

            createResult.IsSuccess.Should().BeFalse();
            createResult.Errors.First().Should().Be($"Organization with name {dto.Name} already exists.");
        }

        // In real world, would write way more tests especially integration tests.
    }
}