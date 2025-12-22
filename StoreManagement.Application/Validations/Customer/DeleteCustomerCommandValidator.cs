using FluentValidation;
using StoreManagement.Application.Customer.Command;

namespace StoreManagement.Application.Validations.Customer
{
    public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidator()
        {
            RuleFor(command => command.CompanyId)
                .GreaterThan(0)
                .WithMessage("Company ID must be greater than zero.");

            RuleFor(c => c.Id)
                .GreaterThan(0)
                .WithMessage("Customer Id must be greater than zero.");
        }
    }
}
