using Documentmanager.Api.Controllers.Common;
using Documentmanager.Core.Domain.Dtos.Organizations;
using Documentmanager.Core.Domain.Dtos.OrganizationUser;
using Documentmanager.Core.Domain.Services.Organizations;
using Documentmanager.Core.Domain.Services.OrganizationUser;
using Documentmanager.Core.Domain.Services.Users;
using Microsoft.AspNetCore.Mvc;


namespace Documentmanager.Api.Controllers
{
    [ApiController]
    [Route("v1/admin/[controller]")]
    public class OrganizationUserController : CommonController
    {
        private readonly OrganizationUserService _service;

        private readonly ILogger<OrganizationUserController> _logger;

        public OrganizationUserController(OrganizationUserService service, ILogger<OrganizationUserController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPatch]
        public async Task<ActionResult<int>> UpdateUserOrganization([FromBody] UpdateOrganizationUserDto request, [FromHeader(Name = "X-User-Id")] int userId)
        {
            if (userId == default(int))
            {
                return BadRequest("UserId missing from header.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.UpdateUserOrganization(request, userId);

            return result.IsSuccess ? OkFromResult(result) : BadRequestFromResult(result);
        }
    }
}