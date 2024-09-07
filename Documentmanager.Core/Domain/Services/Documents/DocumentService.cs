using AutoMapper;
using Documentmanager.Core.Domain.Dtos.Documents;
using Documentmanager.Core.Domain.Models.Common;
using Documentmanager.Core.Domain.Models.Document;
using Documentmanager.Core.Domain.Repositories.Interfaces;
using Documentmanager.Core.Domain.Services.Common;
using Microsoft.Extensions.Logging;

namespace Documentmanager.Core.Domain.Services.Documents
{
    public class DocumentService
    {
        private readonly IFileService _fileService;
        private readonly IDocumentRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DocumentService> _logger;

        public DocumentService(IFileService fileService, IDocumentRepository repository, IUserRepository userRepository, IMapper mapper, ILogger<DocumentService> logger)
        {
            _fileService = fileService;
            _repository = repository;
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<GetDocumentDto>> FindById(int id, int userId)
        {
            var result = new Result<GetDocumentDto>();
            var existingUser = await _userRepository.GetById(userId);
            if (existingUser == null)
            {
                result.AddError($"User with Id {userId} does not exist.");
                return result;
            }
            var document = await _repository.GetById(id);
            if (document?.UserId != existingUser.Id || (document.OrganizationId.HasValue && document.OrganizationId != existingUser.OrganizationId))
            {
                result.AddError($"Document with id {id} not found.");
                return result;
            }      
            var dto = _mapper.Map<GetDocumentDto>(document);
            result.AddSuccessData(dto);
            return result;
        }

        public async Task<Result<IEnumerable<GetDocumentDto>>> GetAllDocuments(int userId)
        {
            var result = new Result<IEnumerable<GetDocumentDto>>();
            var existingUser = await _userRepository.GetById(userId);
            if (existingUser == null)
            {
                result.AddError($"User with Id {userId} does not exist.");
                return result;
            }
            // Definitely better ways to do this but for brevity, just going to get all documents and filter in mem
            var documents = await _repository.GetAll();
            var documentsFiltered = documents.Where(document => document.UserId == existingUser.Id || (existingUser.OrganizationId != null && document.OrganizationId == existingUser.OrganizationId)).ToList();
            var dtos = _mapper.Map<IEnumerable<GetDocumentDto>>(documents);
            result.AddSuccessData(dtos);
            return result;
        }

        public async Task<Result<int>> CreateDocument(CreateDocumentDto dto, int userId)
        {
            var result = new Result<int>();
            var existingUser = await _userRepository.GetById(userId);
            if (existingUser == null)
            {
                result.AddError($"User with Id {userId} does not exist.");
                return result;
            }
            var storagePath = await _fileService.UploadFile(dto.File);
            var document = new Document(dto.Name, storagePath, existingUser.Id, existingUser.OrganizationId);
            var createdId = await _repository.Create(document);
            _logger.LogInformation($"User {document.CreatedBy} created Document with Id {createdId} and Name {document.Name} at {document.DateCreated}");
            result.AddSuccessData(createdId);
            return result;
        }

        public async Task<Result<int>> UpdateDocument(UpdateDocumentDto dto, int userId)
        {
            var result = new Result<int>();
            var existingUser = await _userRepository.GetById(userId);
            if (existingUser == null)
            {
                result.AddError($"User with Id {userId} does not exist.");
                return result;
            }
            var existingDocument = await _repository.GetById(dto.Id);
            if (existingDocument?.UserId != existingUser.Id || (existingDocument.OrganizationId.HasValue && existingDocument.OrganizationId != existingUser.OrganizationId))
            {
                result.AddError($"Document with id {dto.Id} not found.");
                return result;
            }
            var storagePath = await _fileService.UploadFile(dto.File);
            existingDocument.Update(dto.Name, storagePath, existingUser.Id, existingUser.OrganizationId);
            var updatedId = await _repository.Update(existingDocument);
            _logger.LogInformation($"User {existingDocument.ModifiedBy} updated Document with Id {updatedId} and Name {existingDocument.Name} at {existingDocument.DateModified}");
            result.AddSuccessData(updatedId);
            return result;
        }

        public async Task<Result<int>> DeleteDocument(int id, int userId)
        {
            var result = new Result<int>();
            var existingUser = await _userRepository.GetById(userId);
            if (existingUser == null)
            {
                result.AddError($"User with id {id} not found.");
                return result;
            }
            var existingDocument = await _repository.GetById(id);
            if (existingDocument?.UserId != existingUser.Id || (existingDocument.OrganizationId.HasValue && existingDocument.OrganizationId != existingUser.OrganizationId))
            {
                result.AddError($"Document with id {id} not found.");
                return result;
            }
            await _fileService.DeleteFile(existingDocument.StoragePath);
            await _repository.Delete(existingDocument);
            result.AddSuccessData(existingDocument.Id);
            _logger.LogInformation($"User {existingUser.Id} deleted Document with Id {existingDocument.Id} and Name {existingDocument.Name} at {DateTime.UtcNow}");
            return result;
        }
    }
}
