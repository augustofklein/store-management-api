using FluentValidation;
using StoreManagement.Application.Customer.Model.Dto;

namespace StoreManagement.Application.Validations.Customer
{
    public class CustomerContactUpsertDtoValidator : AbstractValidator<CustomerContactCommandDto>
    {
        public CustomerContactUpsertDtoValidator()
        {
            RuleFor(c => c.ContactType)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Contact type must be greater than zero.");

            RuleFor(c => c.ContactDescription)
                .NotEmpty()
                .WithMessage("Contact description is required.")
                .MaximumLength(50)
                .WithMessage("Contact description must not exceed 50 characters.");
        }
    }
}
