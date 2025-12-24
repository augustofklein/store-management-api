using FluentValidation;
using StoreManagement.Application.Customer.Command;

namespace StoreManagement.Application.Validations.Customer
{
    public class AddCustomerCommandValidator : AbstractValidator<AddCustomerCommand>
    {
        public AddCustomerCommandValidator()
        {
            RuleFor(c => c.Identification)
                .NotEmpty()
                .WithMessage("Sku Id is required.")
                .MaximumLength(18)
                .WithMessage("Sku Id must not exceed 18 characters.");

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(100)
                .WithMessage("Name must not exceed 100 characters.");

            RuleFor(c => c.Address)
                .NotEmpty()
                .WithMessage("Address is required.")
                .MaximumLength(100)
                .WithMessage("Address must not exceed 100 characters.");
        }
    }
}
