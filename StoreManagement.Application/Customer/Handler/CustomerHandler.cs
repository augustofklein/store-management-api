using CSharpFunctionalExtensions;
using MediatR;
using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Application.Customer.Command;
using StoreManagement.Application.Customer.Model.Dto;
using StoreManagement.Application.Customer.Service;

namespace StoreManagement.Application.Customer.Handler
{
    public class CustomerHandler(ICustomerRepository customerRepository, ICustomerService customerService, IEFTransactionManager eFTransactionManager) :
        IRequestHandler<AddCustomerCommand, Result>
    {
        public async Task<Result> Handle(AddCustomerCommand command, CancellationToken cancellationToken)
        {
            var validation = await customerService.ValidateAddCustomerAsync(command.CompanyId, command.Identification, cancellationToken);
            if (validation.IsFailure)
                return Result.Failure(validation.Error);

            var customer = new CustomerDto
            {
                Id = 0,
                Identification = command.Identification,
                Address = command.Address,
                CustomerContacts = [.. command.CustomerContacts.Select(c => new CustomerContactDto
                {
                    ContactId = 0,
                    ContactType = c.ContactType,
                    Description = string.Empty,
                    Contact = c.ContactDescription
                })]
            };

            var result = await customerRepository.AddCustomerAsync(command.CompanyId, customer, cancellationToken);

            return Result.Success();
        }
    }
}
