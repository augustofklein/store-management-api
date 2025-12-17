using FluentValidation;
using StoreManagement.Application.Product.Command;

namespace StoreManagement.Application.Validations;

public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    public AddProductCommandValidator()
    {
        RuleFor(c => c.SkuId).NotNull();
        RuleFor(c => c.Status).NotNull();
        RuleFor(c => c.Barcode).NotNull();
        RuleFor(c => c.Description).NotNull();
        RuleFor(c => c.Stock).NotNull();
    }
}