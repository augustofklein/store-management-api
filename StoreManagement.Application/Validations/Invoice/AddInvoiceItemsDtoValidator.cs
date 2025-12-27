using FluentValidation;
using StoreManagement.Application.Invoice.Model;

namespace StoreManagement.Application.Validations.Invoice
{
    public class AddInvoiceItemsDtoValidator : AbstractValidator<AddInvoiceItemsDto>
    {
        public AddInvoiceItemsDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThanOrEqualTo(0)
                .WithMessage("ProductId must be greater than or equal to zero.");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than zero.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than zero.");
        }
    }
}
