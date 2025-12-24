using FluentValidation;
using StoreManagement.Application.Product.Command;

namespace StoreManagement.Application.Validations.Product
{
    public class DeleteProductCommandValidator : AbstractValidator<RemoveProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(command => command.Id)
                .NotEmpty()
                .WithMessage("Id is required.")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Product ID must be greater than zero.");
        }
    }
}
