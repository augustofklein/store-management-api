using FluentValidation;
using StoreManagement.Application.Invoice.Command;

namespace StoreManagement.Application.Validations.Invoice
{
    public class AddInvoiceCommandValidator : AbstractValidator<AddInvoiceCommand>
    {
        public AddInvoiceCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty()
                .WithMessage("Customer Id is required.")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Company Id must be greater or equal to zero.");

            RuleForEach(x => x.InvoiceItems)
                .SetValidator(new AddInvoiceItemsDtoValidator());

        }
    }
}
