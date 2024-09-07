using Documentmanager.Api.Controllers.Common;
using Documentmanager.Core.Domain.Dtos.Organizations;
using Documentmanager.Core.Domain.Services.Organizations;
using Microsoft.AspNetCore.Mvc;


namespace Documentmanager.Api.Controllers
{
    [ApiController]
    [Route("v1/admin/[controller]")]
    public class OrganizationsController : CommonController
    {
        private readonly OrganizationService _service;

        private readonly ILogger<OrganizationsController> _logger;

        public OrganizationsController(OrganizationService service, ILogger<OrganizationsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetOrganizationDto>> Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.FindById(id);
            return result.IsSuccess ? OkFromResult(result) : BadRequestFromResult(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetOrganizationDto>>> GetAllOrganizations()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.GetAllOrganizations();
            return result.IsSuccess ? OkFromResult(result) : BadRequestFromResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateOrganization([FromBody] CreateOrganizationDto request, [FromHeader(Name = "X-User-Id")] int userId)
        {
            if (userId == default(int))
            {
                return BadRequest("UserId missing from header.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.CreateOrganization(request, userId);

            return result.IsSuccess ? OkFromResult(result) : BadRequestFromResult(result);
        }

        [HttpPut]
        public async Task<ActionResult<int>> UpdateOrganization([FromBody] UpdateOrganizationDto request, [FromHeader(Name = "X-User-Id")] int userId)
        {
            if (userId == default(int))
            {
                return BadRequest("UserId missing from header.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.UpdateOrganization(request, userId);

            return result.IsSuccess ? OkFromResult(result) : BadRequestFromResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteOrganization(int id, [FromHeader(Name = "X-User-Id")] int userId)
        {
            if (userId == default(int))
            {
                return BadRequest("UserId missing from header.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.DeleteOrganization(id);

            return result.IsSuccess ? OkFromResult(result) : BadRequestFromResult(result);
        }
    }
}