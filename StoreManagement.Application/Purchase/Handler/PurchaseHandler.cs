using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Application.Purchase.Command;
using StoreManagement.Application.Purchase.Model;
using StoreManagement.Application.Purchase.Service;
using StoreManagement.Application.Purchase.XML;
using System.Xml.Linq;

namespace StoreManagement.Application.Purchase.Handler
{
    public class PurchaseHandler(IPurchaseService purchaseService, IPurchaseRepository purchaseRepository, IMapper mapper) :
        IRequestHandler<PreviewPurchaseXmlCommand, Result<PurchasePreviewDto>>,
        IRequestHandler<AddPurchaseCommand, Result>
    {
        public async Task<Result<PurchasePreviewDto>> Handle(PreviewPurchaseXmlCommand command, CancellationToken cancellationToken)
        {
            XDocument xml;

            using (var stream = command.File.OpenReadStream())
            {
                xml = XDocument.Load(stream);
            }

            var preview = PurchaseXmlPreviewMapper.Map(xml);

            if (!preview.Products.Any())
                return Result.Failure<PurchasePreviewDto>("No products found in XML.");

            var validation = await purchaseService.EnrichAndValidatePurchasePreviewAsync(command.CompanyId, preview, cancellationToken);
            if (validation.IsFailure)
                return Result.Failure<PurchasePreviewDto>(validation.Error);

            return Result.Success(preview);
        }

        public async Task<Result> Handle(AddPurchaseCommand command, CancellationToken cancellationToken)
        {
            var validation = await purchaseService.ValidateAddPurchaseAsync(command, cancellationToken);
            if(validation.IsFailure)
                return Result.Failure(validation.Error);

            var result = await purchaseRepository.AddPurchaseAsync(command.CompanyId, mapper.Map<AddPurchaseDto>(command), cancellationToken);
            if (result.IsFailure)
                return Result.Failure(result.Error);

            return Result.Success();
        }
    }
}
