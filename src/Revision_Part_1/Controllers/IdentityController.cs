using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Revision_Part_1.Application.CQRS.Commands;

namespace Revision_Part_1.Controllers
{
    [Route("api/identity")]
    [ApiController]
    public class IdentityController(IMediator mediator) : ControllerBase
    {
        [HttpPatch("user")]
        [Authorize]
        public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpPost("userRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("userRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UnassignUserRole(UnassignUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }
    }
}
