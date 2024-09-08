using AutoMapper;
using Documentmanager.Core.Domain.Dtos.Documents;
using Documentmanager.Core.Domain.Models.Document;
using Documentmanager.Core.Domain.Models.Users;
using Documentmanager.Core.Domain.Repositories.Interfaces;
using Documentmanager.Core.Domain.Services.Common;
using Documentmanager.Core.Domain.Services.Documents;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace Documentmanager.Api.Test.Unit
{
    public class DocumentsTest
    {
        [Fact(DisplayName = "Should create Documents")]
        public async Task ShouldCreateDocuments()
        {
            var dto = new CreateDocumentDto
            {
                Name = "Some Doc"
            };
            var userId = 123;
            var storagePath = "SomeStoragePath";
            var returnedId = 234;
            var userRepository = Substitute.For<IUserRepository>();
            userRepository
                .GetById(123)
                .Returns(new User("something@email.com", userId));
            var documentRepository = Substitute.For<IDocumentRepository>();
            documentRepository
                .Create(Arg.Any<Document>())
                .Returns(returnedId);
            var fileService = Substitute.For<IFileService>();
            fileService
                .UploadFile(Arg.Any<IFormFile>()).Returns(storagePath);
            var loggerService = Substitute.For<ILogger<DocumentService>>();
            var mapper = Substitute.For<IMapper>();
            var documentService = new DocumentService(fileService, documentRepository, userRepository, mapper, loggerService);

            var createResult = await documentService.CreateDocument(dto, userId);

            createResult.SuccessData.Should().Be(returnedId);
        }

        // In real world, would write way more tests especially integration tests.
    }
}