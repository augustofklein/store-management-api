using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Application.Customer.Command;
using StoreManagement.WebApi.Extensions;

namespace StoreManagement.WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [ApiVersion("1")]
    [Route("v{version:ApiVersion}/[controller]")]
    public class CustomerController(IMediator mediator, ICustomerRepository customerRepository) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] AddCustomerCommand command, CancellationToken cancellationToken)
        {
            command.CompanyId = User.GetCompanyId();

            var response = await mediator.Send(command, cancellationToken);
            if (response.IsFailure)
                return BadRequest(response.Error);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCustomer(int id, [FromBody] EditCustomerCommand command, CancellationToken cancellationToken)
        {
            command.CompanyId = User.GetCompanyId();
            command.Id = id;

            var response = await mediator.Send(command, cancellationToken);
            if (response.IsFailure)
                return BadRequest(response.Error);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id, CancellationToken cancellationToken)
        {
            var command = new DeleteCustomerCommand(User.GetCompanyId(), id);

            var response = await mediator.Send(command, cancellationToken);
            if (response.IsFailure)
                return BadRequest(response.Error);

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers(CancellationToken cancellationToken, int pageNumber = 1, int pageSize = 10)
        {
            var response = await customerRepository.ReturnAllCustomersAsync(User.GetCompanyId(), pageNumber, pageSize, cancellationToken);
            if (response.Value == null)
            {
                return NotFound();
            }

            return Ok(response.Value);
        }
    }
}
