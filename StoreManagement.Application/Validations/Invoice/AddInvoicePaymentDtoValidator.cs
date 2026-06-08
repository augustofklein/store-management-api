using FluentValidation;
using StoreManagement.Application.Payments.Model;

namespace StoreManagement.Application.Validations.Invoice
{
    internal class AddInvoicePaymentDtoValidator : AbstractValidator<AddInvoicePaymentDto>
    {
        public AddInvoicePaymentDtoValidator()
        {
            RuleFor(x => x.PaymentTypeId)
                .GreaterThan(0)
                .WithMessage("PaymentTypeId must be greater than zero.");

            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Payment amount must be greater than zero.");
        }
    }
}
