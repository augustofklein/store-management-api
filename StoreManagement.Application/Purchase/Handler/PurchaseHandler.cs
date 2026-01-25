using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Application.Product.Model;
using StoreManagement.Application.Purchase.Command;
using StoreManagement.Application.Purchase.Model;
using StoreManagement.Application.Purchase.Service;
using StoreManagement.Application.Purchase.XML;
using StoreManagement.Domain.Enums;
using System.Xml.Linq;

namespace StoreManagement.Application.Purchase.Handler
{
    public class PurchaseHandler(IPurchaseService purchaseService, IPurchaseRepository purchaseRepository, IProductRepository productRepository, IMapper mapper, IEFTransactionManager eFTransactionManager) :
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

            await eFTransactionManager.BeginAsync(cancellationToken);

            try
            {
                var result = await purchaseRepository.AddPurchaseAsync(command.CompanyId, mapper.Map<AddPurchaseDto>(command), cancellationToken);

                var updateAverageCostResult = await productRepository.UpdateProductAverageCostArrayAsync(command.CompanyId, mapper.Map<List<AddProductMovementDto>>(command.Products), cancellationToken);
                if (updateAverageCostResult.IsFailure)
                    return Result.Failure(updateAverageCostResult.Error);

                var productMovementResult = await productRepository.UpdateProductStockArrayAsync(command.CompanyId, ProductMovementEnum.PURCHASE, mapper.Map<List<UpdateProductStockDto>>(command.Products), cancellationToken);
                if (productMovementResult.IsFailure)
                    return Result.Failure(productMovementResult.Error);

                await productRepository.AddProductMovementArrayAsync(ProductMovementEnum.PURCHASE, command.PurchaseEntryDate, mapper.Map<List<AddProductMovementDto>>(command.Products), cancellationToken);

                await eFTransactionManager.CommitAsync(cancellationToken);
            }
            catch(Exception ex)
            {
                await eFTransactionManager.RollbackAsync(cancellationToken);
                return Result.Failure($"An error occurred while adding the purchase: {ex.Message}");
            }


            return Result.Success();
        }
    }
}
