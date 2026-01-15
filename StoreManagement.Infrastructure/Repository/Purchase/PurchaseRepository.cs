using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Application.Product.Model;
using StoreManagement.Application.Purchase.Model;
using StoreManagement.Domain.Entities;
using StoreManagement.Domain.Enums;
using StoreManagement.Infrastructure.DBContext;

namespace StoreManagement.Infrastructure.Repository.Purchase
{
    public class PurchaseRepository(AppDbContext dbContext, IProductRepository productRepository, IMapper mapper, IEFTransactionManager eFTransactionManager) : IPurchaseRepository
    {
        public async Task<Result<IEnumerable<PurchaseDto>>> ReturnAllPuchasesAsync(int companyId, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await dbContext.Purchase
                .Include(i => i.PurchaseItems)
                    .ThenInclude(ii => ii.Product)
                .Where(i => i.CompanyId == companyId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(i => new PurchaseDto
                {
                    PurchaseId = i.Id,
                    PurchaseDate = i.PurchaseDate,
                    TotalAmount = i.TotalAmount,
                    Supplier = new PurchaseDto.SupplierPurchase
                    {
                        Id = i.Supplier.Id,
                        DocumentNumber = i.Supplier.DocumentNumber,
                        Name = i.Supplier.Name,
                    },
                    Items = i.PurchaseItems.Select(ii => new PurchaseDto.PurchaseItem
                    {
                        ProductId = ii.ProductId,
                        SkuId = ii.Product.SkuId,
                        Barcode = ii.Product.Barcode,
                        Description = ii.Product.Description,
                        Price = ii.Price,
                        Quantity = ii.Quantity
                    }).ToList()
                }).ToListAsync(cancellationToken);
        }

        public async Task<bool> ValidateExistsPurchaseByDocumentKey(int companyId, string documentId, CancellationToken cancellationToken)
        {
            return await dbContext.Purchase
                .AnyAsync(i => i.CompanyId == companyId && i.DocumentKey == documentId, cancellationToken);
        }

        public async Task<Result> AddPurchaseAsync(int companyId, AddPurchaseDto purchase, CancellationToken cancellationToken)
        {
            await eFTransactionManager.BeginAsync(cancellationToken);

            try
            {
                var purchaseEntity = new PurchaseEntity
                {
                    CompanyId = companyId,
                    SupplierId = purchase.SupplierId,
                    DocumentNumber = purchase.Document.DocumentNumber,
                    DocumentSerie = purchase.Document.DocumentSerie,
                    DocumentMod = purchase.Document.DocumentMod,
                    DocumentKey = purchase.Document.DocumentKey,
                    PurchaseDate = purchase.Document.DocumentDate,
                    PurchaseEntryDate = purchase.PurchaseEntryDate,
                    TotalAmount = purchase.Products.Sum(i => i.Price * i.Quantity),
                    PurchaseItems = [.. purchase.Products.Select(i => new PurchaseItemEntity
                    {
                        ProductId = i.ProductId,
                        Price = i.Price,
                        Package = i.Package,
                        Quantity = i.Quantity,
                        ShippingCost = i.ShippingCost
                    })]
                };

                await dbContext.Purchase.AddAsync(purchaseEntity, cancellationToken);

                var updateAverageCostResult = await productRepository.UpdateProductAverageCostArrayAsync(companyId, mapper.Map<List<AddProductMovementDto>>(purchase.Products), cancellationToken);
                if(updateAverageCostResult.IsFailure)
                    return Result.Failure(updateAverageCostResult.Error);

                var productMovementResult = await productRepository.UpdateProductStockArrayAsync(companyId, ProductMovementEnum.PURCHASE, mapper.Map<List<UpdateProductStockDto>>(purchase.Products), cancellationToken);
                if(productMovementResult.IsFailure)
                    return Result.Failure(productMovementResult.Error);

                await productRepository.AddProductMovementArrayAsync(ProductMovementEnum.PURCHASE, purchase.PurchaseEntryDate, mapper.Map<List<AddProductMovementDto>>(purchase.Products), cancellationToken);

                await eFTransactionManager.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await eFTransactionManager.RollbackAsync(cancellationToken);
                return Result.Failure($"An error occurred while adding the purchase: {ex.Message}");
            }

            return Result.Success();
        }
    }
}
