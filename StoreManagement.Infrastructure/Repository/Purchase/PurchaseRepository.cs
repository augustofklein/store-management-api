using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Application.Purchase.Model;
using StoreManagement.Infrastructure.DBContext;

namespace StoreManagement.Infrastructure.Repository.Purchase
{
    public class PurchaseRepository(AppDbContext dbContext) : IPurchaseRepository
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
                        Identification = i.Supplier.Identification,
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
    }
}
