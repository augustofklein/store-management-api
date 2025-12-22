using CSharpFunctionalExtensions;
using MediatR;
using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Application.Customer.Command;
using StoreManagement.Application.Customer.Model.Dto;
using StoreManagement.Application.Customer.Service;

namespace StoreManagement.Application.Customer.Handler
{
    public class CustomerHandler(ICustomerRepository customerRepository, ICustomerService customerService) :
        IRequestHandler<AddCustomerCommand, Result>,
        IRequestHandler<DeleteCustomerCommand, Result>,
        IRequestHandler<EditCustomerCommand, Result>
    {
        public async Task<Result> Handle(AddCustomerCommand command, CancellationToken cancellationToken)
        {
            var validation = await customerService.ValidateCustomerNotExistsAsync(command.CompanyId, command.Identification, cancellationToken);
            if (validation.IsFailure)
                return Result.Failure(validation.Error);

            var customer = new CustomerDto
            {
                Id = 0,
                Identification = command.Identification,
                Name = command.Name,
                Address = command.Address,
                CustomerContacts = [.. command.CustomerContacts.Select(c => new CustomerContactDto
                {
                    ContactId = 0,
                    ContactType = c.ContactType,
                    Description = string.Empty,
                    Contact = c.ContactDescription
                })]
            };

            return Result.Success(await customerRepository.AddCustomerAsync(command.CompanyId, customer, cancellationToken));
        }

        public async Task<Result> Handle(EditCustomerCommand command, CancellationToken cancellationToken)
        {
            var validation = await customerService.ValidateCustomerExistsByIdAsync(command.CompanyId, command.Id, cancellationToken);
            if (validation.IsFailure)
                return Result.Failure(validation.Error);
            
            var customer = new CustomerDto
            {
                Id = command.Id,
                Identification = string.Empty,
                Name = command.Name,
                Address = command.Address,
                CustomerContacts = [.. command.CustomerContacts.Select(c => new CustomerContactDto
                {
                    ContactId = 0,
                    ContactType = c.ContactType,
                    Description = string.Empty,
                    Contact = c.ContactDescription
                })]
            };
            
            return Result.Success(await customerRepository.UpdateCustomerAsync(command.CompanyId, customer, cancellationToken));
        }

        public async Task<Result> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
        {
            var validation = await customerService.ValidateCustomerExistsByIdAsync(command.CompanyId, command.Id, cancellationToken);
            if (validation.IsFailure)
                return Result.Failure(validation.Error);

            return Result.Success(await customerRepository.DeleteCustomerAsync(command.CompanyId, command.Id, cancellationToken));
        }
    }
}
