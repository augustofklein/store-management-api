using FluentValidation;
using StoreManagement.Application.Customer.Command;

namespace StoreManagement.Application.Validations.Customer
{
    public class EditCustomerCommandValidator : AbstractValidator<EditCustomerCommand>
    {
        public EditCustomerCommandValidator()
        {
            RuleFor(c => c.Id)
                .GreaterThan(0)
                .WithMessage("Customer Id must be greater than zero.");

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Customer name is required.")
                .MaximumLength(100)
                .WithMessage("Customer name must not exceed 100 characters.");

            RuleFor(c => c.Address)
                .NotEmpty()
                .WithMessage("Customer address is required.")
                .MaximumLength(100)
                .WithMessage("Customer address must not exceed 100 characters.");

            RuleFor(c => c.CustomerContacts)
                .NotNull()
                .WithMessage("Customer contacts are required.");

            RuleForEach(c => c.CustomerContacts)
                .SetValidator(new CustomerContactUpsertDtoValidator());
        }
    }
}
