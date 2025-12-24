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
                .WithMessage("Sku Id is required.")
                .MaximumLength(20)
                .WithMessage("Sku Id must not exceed 13 characters.");

            RuleFor(c => c.Barcode)
                .NotEmpty()
                .WithMessage("Barcode is required.")
                .MaximumLength(13)
                .WithMessage("Barcode must not exceed 13 characters.");

            RuleFor(c => c.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MaximumLength(50)
                .WithMessage("Description must not exceed 50 characters.");

            RuleFor(c => c.Stock)
                .NotEmpty()
                .WithMessage("Stock is required.")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Stock cannot be negative.");

            RuleFor(c => c.Price)
                .NotEmpty()
                .WithMessage("Price is required.")
                .GreaterThan(0)
                .WithMessage("Price must be greater than zero.");
        }
    }
}