using FluentValidation;
using StoreManagement.Application.Customer.Command;

namespace StoreManagement.Application.Validations.Customer
{
    public class AddCustomerCommandValidator : AbstractValidator<AddCustomerCommand>
    {
        public AddCustomerCommandValidator()
        {
            RuleFor(c => c.Identification).NotNull();
            RuleFor(c => c.Name).NotNull();
            RuleFor(c => c.Address).NotNull();
        }
    }
}
