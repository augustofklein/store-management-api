using FluentValidation;
using StoreManagement.Application.Product.Command;

namespace StoreManagement.Application.Validations;

public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    public AddProductCommandValidator()
    {
        RuleFor(command => command.SkuId).NotNull();
        RuleFor(command => command.Status).NotNull();
        RuleFor(command => command.Barcode).NotNull();
        RuleFor(command => command.Description).NotNull();
        RuleFor(command => command.Stock).NotNull();
    }
}