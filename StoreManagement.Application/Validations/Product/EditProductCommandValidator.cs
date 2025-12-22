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
                .MaximumLength(50);
        }
    }
}
