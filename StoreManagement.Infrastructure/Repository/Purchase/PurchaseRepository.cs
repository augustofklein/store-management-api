using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Application.Purchase.Model;
using StoreManagement.Domain.Entities;
using StoreManagement.Infrastructure.DBContext;

namespace StoreManagement.Infrastructure.Repository.Purchase
{
    public class PurchaseRepository(AppDbContext dbContext, IProductRepository productRepository) : IPurchaseRepository
    {
        public async Task<Result<IEnumerable<PurchaseDto>>> ReturnAllPuchasesAsync(int companyId, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await dbContext.Purchase
                .AsNoTracking()
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

        public async Task<Result> AddPurchaseAsync(int companyId, AddPurchaseDto purchase, CancellationToken cancellationToken)
        {
            await dbContext.Database.BeginTransactionAsync(cancellationToken);

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
                    PurchaseItems = purchase.Products.Select(i => new PurchaseItemEntity
                    {
                        ProductId = i.Id,
                        Price = i.Price,
                        Package = i.Package,
                        Quantity = i.Quantity,
                        ShippingCost = i.ShippingCost
                    }).ToList()
                };

                await dbContext.Purchase.AddAsync(purchaseEntity, cancellationToken);

                foreach(var item in purchase.Products)
                {
                    await productRepository.UpdateAverageCostAsync(companyId, item.Id, item.Quantity, item.Price, cancellationToken);
                }

                await dbContext.SaveChangesAsync(cancellationToken);
                await dbContext.Database.CommitTransactionAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await dbContext.Database.RollbackTransactionAsync(cancellationToken);
                return Result.Failure($"An error occurred while adding the purchase: {ex.Message}");
            }

            return Result.Success();
        }
    }
}
