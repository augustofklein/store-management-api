using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Auth.Command;
using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Common.Constants;
using StoreManagement.WebApi.InputModel.User;
using StoreManagement.WebApi.SwaggerConfiguration;
using System.Text;

namespace StoreManagement.WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("v{version:ApiVersion}/[controller]")]
    public class AuthController(IMediator mediator, IAuthService authService) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("login")]
        [RequireBasicAuth]
        public async Task<IActionResult> LoginAsync(CancellationToken cancellationToken)
        {
            var credentials = ExtractBasicAuthCredentials();
            
            if (credentials == null)
                return BadRequest("Invalid or missing Basic Auth credentials");
            
            var response = await authService.EnsureUserExistsAsync(credentials.Username, credentials.Password, cancellationToken);
            if (response.IsFailure)
                return Unauthorized(response.Error);
            
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("companies")]
        [RequireBasicAuth]
        public async Task<IActionResult> GetUserCompanies(CancellationToken cancellationToken)
        {
            var credentials = ExtractBasicAuthCredentials();

            var response = await authService.GetUserCompaniesAsync(credentials.Username, credentials.Password, cancellationToken);
            if (response.IsFailure)
                return Unauthorized(response.Error);

            return Ok(response.Value);
        }

        [AllowAnonymous]
        [HttpPost("token")]
        [RequireBasicAuth]
        public async Task<IActionResult> AuthenticateAsync(CancellationToken cancellationToken)
        {
            var credentials = ExtractBasicAuthWithCompanyCredentials();
            
            if (credentials == null)
                return BadRequest("Invalid or missing Basic Auth credentials");
            
            var command = new AuthCommand(credentials.Username, credentials.Password, credentials.CompanyId);
            var response = await mediator.Send(command, cancellationToken);

            if(response.IsFailure)
                return BadRequest(response.Error);

            return Ok(response.Value);
        }
        
        private UserToken? ExtractBasicAuthWithCompanyCredentials()
        {
            var authHeader = Request.Headers.Authorization.FirstOrDefault();
            var companyHeader = Request.Headers[HttpHeadersConstants.CompanyId].FirstOrDefault();

            if (authHeader == null || !authHeader.StartsWith("Basic ") || companyHeader == null)
                return null;

            try
            {
                var encodedCredentials = authHeader.Substring("Basic ".Length).TrimStart();
                var decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));
                var credentials = decodedCredentials.Split(':', 3);
        
                return credentials.Length == 2 ? new UserToken(credentials[0], credentials[1], Int32.Parse(companyHeader)) : null;
            }
            catch
            {
                return null;
            }
        }

        private UserCredencials? ExtractBasicAuthCredentials()
        {
            var authHeader = Request.Headers.Authorization.FirstOrDefault();

            if (authHeader == null || !authHeader.StartsWith("Basic "))
                return null;

            try
            {
                var encodedCredentials = authHeader.Substring("Basic ".Length).TrimStart();
                var decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));
                var credentials = decodedCredentials.Split(':', 3);

                return credentials.Length == 2 ? new UserCredencials(credentials[0], credentials[1]) : null;
            }
            catch
            {
                return null;
            }
        }
    }
}
