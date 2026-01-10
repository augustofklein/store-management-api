using FluentValidation;
using StoreManagement.Application.Customer.Command;

namespace StoreManagement.Application.Validations.Customer
{
    public class AddCustomerCommandValidator : AbstractValidator<AddCustomerCommand>
    {
        public AddCustomerCommandValidator()
        {
            RuleFor(c => c.DocumentNumber)
                .NotEmpty()
                .WithMessage("Document number is required.")
                .MaximumLength(11)
                .WithMessage("Document number must not exceed 11 characters.");

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
