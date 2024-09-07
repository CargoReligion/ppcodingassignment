using Documentmanager.Api.Controllers.Common;
using Documentmanager.Core.Domain.Dtos.Documents;
using Documentmanager.Core.Domain.Services.Documents;
using Microsoft.AspNetCore.Mvc;


namespace Documentmanager.Api.Controllers
{
    [ApiController]
    [Route("v1/admin/[controller]")]
    public class DocumentsController : CommonController
    {
        private readonly DocumentService _service;

        private readonly ILogger<DocumentsController> _logger;

        public DocumentsController(DocumentService service, ILogger<DocumentsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetDocumentDto>> Get(int id, [FromHeader(Name = "X-User-Id")] int userId)
        {
            if (userId == default(int))
            {
                return BadRequest("UserId missing from header.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.FindById(id, userId);
            return result.IsSuccess ? OkFromResult(result) : BadRequestFromResult(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetDocumentDto>>> GetAllDocuments([FromHeader(Name = "X-User-Id")] int userId)
        {
            if (userId == default(int))
            {
                return BadRequest("UserId missing from header.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.GetAllDocuments(userId);
            return result.IsSuccess ? OkFromResult(result) : BadRequestFromResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateDocument([FromForm] CreateDocumentDto request, [FromHeader(Name = "X-User-Id")] int userId)
        {
            if (userId == default(int))
            {
                return BadRequest("UserId missing from header.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.CreateDocument(request, userId);

            return result.IsSuccess ? OkFromResult(result) : BadRequestFromResult(result);
        }

        [HttpPut]
        public async Task<ActionResult<int>> UpdateDocument([FromForm] UpdateDocumentDto request, [FromHeader(Name = "X-User-Id")] int userId)
        {
            if (userId == default(int))
            {
                return BadRequest("UserId missing from header.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.UpdateDocument(request, userId);

            return result.IsSuccess ? OkFromResult(result) : BadRequestFromResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteDocument(int id, [FromHeader(Name = "X-User-Id")] int userId)
        {
            if (userId == default(int))
            {
                return BadRequest("UserId missing from header.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.DeleteDocument(id, userId);

            return result.IsSuccess ? OkFromResult(result) : BadRequestFromResult(result);
        }
    }
}