using FluentValidation;
using StoreManagement.Application.Product.Command;

namespace StoreManagement.Application.Validations.Product
{
    public class EditProductCommandValidator : AbstractValidator<EditProductCommand>
    {
        public EditProductCommandValidator()
        {
            RuleFor(c => c.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MaximumLength(100)
                .WithMessage("Description must not exceed 100 characters.");
        }
    }
}
