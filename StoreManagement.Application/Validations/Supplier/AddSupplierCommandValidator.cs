using FluentValidation;
using StoreManagement.Application.Supplier.Command;

namespace StoreManagement.Application.Validations.Supplier
{
    public class AddSupplierCommandValidator : AbstractValidator<AddSupplierCommand>
    {
        public AddSupplierCommandValidator()
        {
            RuleFor(x => x.Identification)
                .NotEmpty()
                .WithMessage("Identification is required.")
                .MaximumLength(50)
                .WithMessage("Identification must not exceed 18 characters.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(100)
                .WithMessage("Name must not exceed 100 characters.");
        }
    }
}
