using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Auth.Command;
using StoreManagement.WebApi.Model;

namespace StoreManagement.WebApi.Controllers
{
    [ApiController]
    [Route("v{version:ApiVersion}/[controller]")]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [MapToApiVersion("1")]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user, CancellationToken cancellationToken)
        {
            var command = new AuthCommand(user.Username, user.Password);

            var response = await _mediator.Send(command, cancellationToken);

            if(response.IsFailure)
                return BadRequest(response.Error);

            return Ok(response);
        }
    }
}
