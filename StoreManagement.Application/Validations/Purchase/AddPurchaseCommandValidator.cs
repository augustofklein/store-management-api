using FluentValidation;
using StoreManagement.Application.Purchase.Command;

namespace StoreManagement.Application.Validations.Purchase
{
    public class AddPurchaseCommandValidator : AbstractValidator<AddPurchaseCommand>
    {
        public AddPurchaseCommandValidator()
        {
            RuleFor(x => x.CompanyId)
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");

            RuleFor(x => x.Document.DocumentNumber)
                .NotEmpty()
                .WithMessage("Document number is required.")
                .MaximumLength(14);

            RuleFor(x => x.Document.DocumentSerie)
                .NotEmpty()
                .WithMessage("Document serie is required.")
                .MaximumLength(10);

            RuleFor(x => x.Document.DocumentMod)
                .NotEmpty()
                .WithMessage("Document mod is required.")
                .MaximumLength(2);

            RuleFor(x => x.Document.DocumentKey)
                .NotEmpty()
                .WithMessage("Document key is required.")
                .MaximumLength(44)
                .Matches(@"^\d+$")
                .WithMessage("Document key must contain only numbers.");
        }
    }
}
