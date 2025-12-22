using FluentValidation;
using StoreManagement.Application.Product.Command;

namespace StoreManagement.Application.Validations.Product
{
    public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
    {
        public AddProductCommandValidator()
        {
            RuleFor(c => c.SkuId)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(c => c.Barcode)
                .NotEmpty()
                .MaximumLength(13);

            RuleFor(c => c.Description)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(c => c.Stock)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Stock cannot be negative.");
        }
    }
}