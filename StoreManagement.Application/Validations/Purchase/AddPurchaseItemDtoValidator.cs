using FluentValidation;
using StoreManagement.Application.Purchase.Model;

namespace StoreManagement.Application.Validations.Purchase
{
    public class AddPurchaseItemDtoValidator : AbstractValidator<AddPurchaseItemDto>
    {
        public AddPurchaseItemDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage("Product Id is required.")
                .GreaterThan(0)
                .WithMessage("Product Id must be greater than zero.");

            RuleFor(x => x.Price)
                .NotEmpty()
                .WithMessage("Price is required.")
                .GreaterThan(0)
                .WithMessage("Price must be greater than zero.");

            RuleFor(x => x.Quantity)
                .NotEmpty()
                .WithMessage("Quantity is required.")
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than zero.");

            RuleFor(x => x.Package)
                .NotEmpty()
                .WithMessage("Package is required.")
                .GreaterThan(0)
                .WithMessage("Package must be greater than zero.");
        }
    }
}
