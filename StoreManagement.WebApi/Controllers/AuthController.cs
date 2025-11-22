using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Auth.Command;
using StoreManagement.WebApi.InputModel;
using StoreManagement.WebApi.SwaggerConfiguration;

namespace StoreManagement.WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("v{version:ApiVersion}/[controller]")]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        [RequireBasicAuth]
        public async Task<IActionResult> Login(CancellationToken cancellationToken)
        {
            var credentials = ExtractBasicAuthCredentials();
            
            if (credentials == null)
                return BadRequest("Invalid or missing Basic Auth credentials");
            
            var command = new AuthCommand(credentials.Username, credentials.Password, credentials.CompanyId);
            var response = await mediator.Send(command, cancellationToken);

            if(response.IsFailure)
                return BadRequest(response.Error);

            return Ok(response.Value);
        }
        
        private User? ExtractBasicAuthCredentials()
        {
            var authHeader = Request.Headers.Authorization.FirstOrDefault();
            var companyHeader = Request.Headers["CompanyId"].FirstOrDefault();

            if (authHeader == null || !authHeader.StartsWith("Basic ") || companyHeader == null)
                return null;

            try
            {
                var encodedCredentials = authHeader.Substring("Basic ".Length).TrimStart();
                var decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));
                var credentials = decodedCredentials.Split(':', 3);
        
                return credentials.Length == 2 ? new User(credentials[0], credentials[1], Int32.Parse(companyHeader)) : null;
            }
            catch
            {
                return null;
            }
        }
    }
}
