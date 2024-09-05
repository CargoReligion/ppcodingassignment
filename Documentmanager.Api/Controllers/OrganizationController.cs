using Documentmanager.Core.Domain.Dtos;
using Documentmanager.Core.Domain.Services.Organizations;
using Microsoft.AspNetCore.Mvc;


namespace Documentmanager.Api.Controllers
{
    [ApiController]
    [Route("v1/admin/[controller]")]
    public class OrganizationController : ControllerBase
    {
        private readonly OrganizationService _service;

        private readonly ILogger<OrganizationController> _logger;

        public OrganizationController(OrganizationService service, ILogger<OrganizationController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateOrganization([FromBody] CreateOrganizationRequestDto request, [FromHeader(Name = "X-User-Id")] int userId)
        {
            if (userId == default(int))
            {
                return BadRequest("UserId missing from header.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.CreateOrganization(request.Organization, userId);

            return Ok(result);
        }
    }
}