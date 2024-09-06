using Documentmanager.Core.Domain.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace Documentmanager.Api.Controllers.Common
{
    public class CommonController : ControllerBase
    {
        protected ActionResult OkFromResult<T>(Result<T> result) => Ok(result.SuccessData);
        protected ActionResult BadRequestFromResult<T>(Result<T> result) => BadRequest(new { errors = result.Errors});
    }
}
