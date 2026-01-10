using CSharpFunctionalExtensions;
using MediatR;
using StoreManagement.Application.Purchase.Command;
using StoreManagement.Application.Purchase.Model;
using StoreManagement.Application.Purchase.Service;
using StoreManagement.Application.Purchase.XML;
using System.Xml.Linq;

namespace StoreManagement.Application.Purchase.Handler
{
    public class PurchaseHandler(IPurchaseService purchaseService) : IRequestHandler<PreviewPurchaseXmlCommand, Result<PurchasePreviewDto>>
    {
        public async Task<Result<PurchasePreviewDto>> Handle(PreviewPurchaseXmlCommand request, CancellationToken cancellationToken)
        {
            XDocument xml;

            using (var stream = request.File.OpenReadStream())
            {
                xml = XDocument.Load(stream);
            }

            var preview = PurchaseXmlPreviewMapper.Map(xml);

            if (!preview.Products.Any())
                return Result.Failure<PurchasePreviewDto>(
                    "No products found in XML.");

            var validation = await purchaseService.ValidatePurchasePreviewAsync(request.CompanyId, preview, cancellationToken);
            if (validation.IsFailure)
                return Result.Failure<PurchasePreviewDto>(validation.Error);

            return Result.Success(preview);
        }
    }
}
