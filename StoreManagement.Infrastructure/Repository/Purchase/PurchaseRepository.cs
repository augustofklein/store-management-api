using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Application.Purchase.Model;
using StoreManagement.Common;
using StoreManagement.Domain.Entities;
using StoreManagement.Infrastructure.DBContext;

namespace StoreManagement.Infrastructure.Repository.Purchase
{
    public class PurchaseRepository(AppDbContext dbContext) : IPurchaseRepository
    {
        public async Task<Result<IEnumerable<PurchaseDto>>> ReturnAllPuchasesAsync(int companyId, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var purchases = await dbContext.Purchase
                    .Include(i => i.PurchaseItems)
                        .ThenInclude(ii => ii.Product)
                    .Where(i => i.CompanyId == companyId)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(i => new PurchaseDto
                    {
                        PurchaseId = i.Id,
                        PurchaseDate = i.PurchaseDate,
                        PurchaseEntryDate = DateTimeUtils.ToBrazilTime(i.PurchaseEntryDate),
                        TotalAmount = i.TotalAmount,
                        Document = new PurchaseDto.PurchaseDocument
                        {
                            DocumentNumber = i.DocumentNumber,
                            DocumentSerie = i.DocumentSerie,
                            DocumentMod = i.DocumentMod,
                            DocumentKey = i.DocumentKey,
                            DocumentDate = i.PurchaseDate
                        },
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
                            Package = ii.Package,
                            Quantity = ii.Quantity
                        }).ToList()
                    }).ToListAsync(cancellationToken);

                return Result.Success<IEnumerable<PurchaseDto>>(purchases);
            }
            catch(Exception ex)
            {
                return Result.Failure<IEnumerable<PurchaseDto>>($"Error retrieving purchases: {ex.Message}");
            }
        }

        public async Task<bool> ValidateExistsPurchaseByDocumentKey(int companyId, string documentId, CancellationToken cancellationToken)
        {
            return await dbContext.Purchase
                .AnyAsync(i => i.CompanyId == companyId && i.DocumentKey == documentId, cancellationToken);
        }

        public async Task<Result> AddPurchaseAsync(int companyId, AddPurchaseDto purchase, CancellationToken cancellationToken)
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
                PurchaseEntryDate = DateTimeOffset.UtcNow,
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

            return Result.Success();
        }
    }
}
