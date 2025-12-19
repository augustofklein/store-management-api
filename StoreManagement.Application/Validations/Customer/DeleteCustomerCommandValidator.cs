using FluentValidation;
using StoreManagement.Application.Customer.Command;

namespace StoreManagement.Application.Validations.Customer
{
    public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidator()
        {
            RuleFor(c => c.Id)
                .GreaterThan(0)
                .WithMessage("Customer Id must be greater than zero.");
        }
    }
}
