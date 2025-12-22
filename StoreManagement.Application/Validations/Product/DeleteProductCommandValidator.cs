using FluentValidation;
using StoreManagement.Application.Product.Command;

namespace StoreManagement.Application.Validations.Product
{
    public class DeleteProductCommandValidator : AbstractValidator<RemoveProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(command => command.CompanyId)
                .GreaterThan(0)
                .WithMessage("Company ID must be greater than zero.");

            RuleFor(command => command.Id)
                .GreaterThan(0)
                .WithMessage("Product ID must be greater than zero.");
        }
    }
}
