using Documentmanager.Api.Controllers.Common;
using Documentmanager.Core.Domain.Dtos.Users;
using Documentmanager.Core.Domain.Services.Users;
using Microsoft.AspNetCore.Mvc;


namespace Documentmanager.Api.Controllers
{
    [ApiController]
    [Route("v1/admin/[controller]")]
    public class UsersController : CommonController
    {
        private readonly UserService _service;

        private readonly ILogger<UsersController> _logger;

        public UsersController(UserService service, ILogger<UsersController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDto>> Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.FindById(id);
            return result.IsSuccess ? OkFromResult(result) : BadRequestFromResult(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetUserDto>>> GetAllUsers()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.GetAllUsers();
            return result.IsSuccess ? OkFromResult(result) : BadRequestFromResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateUser([FromBody] CreateUserDto request, [FromHeader(Name = "X-User-Id")] int userId)
        {
            if (userId == default(int))
            {
                return BadRequest("UserId missing from header.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.CreateUser(request, userId);

            return result.IsSuccess ? OkFromResult(result) : BadRequestFromResult(result);
        }

        [HttpPut]
        public async Task<ActionResult<int>> UpdateUser([FromBody] UpdateUserDto request, [FromHeader(Name = "X-User-Id")] int userId)
        {
            if (userId == default(int))
            {
                return BadRequest("UserId missing from header.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.UpdateUser(request, userId);

            return result.IsSuccess ? OkFromResult(result) : BadRequestFromResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteUser(int id, [FromHeader(Name = "X-User-Id")] int userId)
        {
            if (userId == default(int))
            {
                return BadRequest("UserId missing from header.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.DeleteUser(id);

            return result.IsSuccess ? OkFromResult(result) : BadRequestFromResult(result);
        }
    }
}