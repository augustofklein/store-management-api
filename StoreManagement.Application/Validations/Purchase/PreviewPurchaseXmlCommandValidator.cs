using FluentValidation;
using StoreManagement.Application.Purchase.Command;

namespace StoreManagement.Application.Validations.Purchase
{
    public class PreviewPurchaseXmlCommandValidator : AbstractValidator<PreviewPurchaseXmlCommand>
    {
        public PreviewPurchaseXmlCommandValidator()
        {
            RuleFor(x => x.File).NotNull();
            RuleFor(x => x.File.Length).GreaterThan(0);
        }
    }
}
