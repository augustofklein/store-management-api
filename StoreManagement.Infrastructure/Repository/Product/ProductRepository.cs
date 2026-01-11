using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Application.Product.Model;
using StoreManagement.Domain.Entities;
using StoreManagement.Domain.Enums;
using StoreManagement.Infrastructure.DBContext;
using StoreManagement.Infrastructure.DBContext.Model;

namespace StoreManagement.Infrastructure.Repository.Product
{
    public class ProductRepository(AppDbContext dbContext) : IProductRepository
    {
        public async Task<Result> AddProductAsync(int companyId, string skuId, bool status, string barcode, string description, int stock, decimal price, CancellationToken cancellationToken)
        {
            var product = new ProductEntity
            {
                CompanyId = companyId,
                Id = 0,
                SkuId = skuId.Trim(),
                Status = status,
                Barcode = barcode,
                Description = description,
                Stock = stock,
                ProductPrice = new ProductPriceEntity
                {
                    Price = price
                }
            };

            await dbContext.Products.AddAsync(product, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result> DeleteProductAsync(int companyId, int id, CancellationToken cancellationToken)
        {
            var product = await dbContext.Products
                .FirstOrDefaultAsync(
                    p => p.CompanyId == companyId && p.Id == id,
                    cancellationToken
                );
            
            if (product == null)
            {
                return Result.Failure($"Product with ID {id} not found.");
            }

            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result> EditProductAsync(int companyId, int id, bool status, string description, CancellationToken cancellationToken)
        {
            var product = await dbContext.Products
                .FirstOrDefaultAsync(
                    p => p.CompanyId == companyId && p.Id == id,
                    cancellationToken
                );

            if (product == null)
            {
                return Result.Failure($"Product with ID {id} not found.");
            }

            product.Description = description;
            product.Status = status;

            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result<IEnumerable<ProductDto>>> GetProductsAsync(int companyId, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await dbContext.Products
                .AsNoTracking()
                .Where(p => p.CompanyId == companyId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    SkuId = p.SkuId,
                    Status = p.Status,
                    Barcode = p.Barcode,
                    Description = p.Description,
                    Stock = p.Stock,
                    Price = p.ProductPrice.Price
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> VerifyProductByIdExistAsync(int companyId, int id, CancellationToken cancellationToken)
        {
            return await dbContext.Products
                .Where(p => p.CompanyId == companyId && p.Id == id)
                .FirstOrDefaultAsync(cancellationToken) != null;
        }

        public async Task<bool> VerifyProductBySkuIdExistAsync(int companyId, string skuId, CancellationToken cancellationToken)
        {
            return await dbContext.Products
                .Where(p => p.CompanyId == companyId && p.SkuId == skuId)
                .FirstOrDefaultAsync(cancellationToken) != null;
        }

        public async Task<Result> VerifyArrayProductsExistAsync(int companyId, IEnumerable<int> productIds, CancellationToken cancellationToken)
        {
            var existingProductIds = await dbContext.Products
                .AsNoTracking()
                .Where(p => p.CompanyId == companyId && productIds.Contains(p.Id))
                .Select(p => p.Id)
                .ToListAsync(cancellationToken);

            var missingProductIds = productIds.Except(existingProductIds).ToList();

            if (missingProductIds.Count != 0)
                return Result.Failure($"The following product IDs do not exist: {string.Join(", ", missingProductIds)}");
            
            return Result.Success();
        }

        public async Task<Result> ValidateProductsByBarcodesAsync(int companyId, IEnumerable<string> barcodeProducts, CancellationToken cancellationToken)
        {
            var existingProductIds = await dbContext.Products
                .AsNoTracking()
                .Where(p => p.CompanyId == companyId && barcodeProducts.Contains(p.Barcode))
                .Select(p => p.Barcode)
                .ToListAsync(cancellationToken);

            var missingProductIds = barcodeProducts.Except(existingProductIds).ToList();

            if (missingProductIds.Count != 0)
                return Result.Failure($"The following barcode products do not exist: {string.Join(", ", missingProductIds)}");

            return Result.Success();
        }

        public async Task AddProductMovementArrayAsync(ProductMovementEnum movementType, DateTime movementDate, List<AddProductMovementDto> items, CancellationToken cancellationToken)
        {
            var productMovements = items.Select(ii => new ProductMovementEntity
            {
                ProductId = ii.ProductId,
                MovementType = movementType,
                Quantity = ii.Quantity,
                Price = ii.Price,
                CreatedAt = movementDate
            });

            await dbContext.ProductMovements.AddRangeAsync(productMovements, cancellationToken);
        }

        public async Task UpdateProductStockArrayAsync(ProductMovementEnum movementType, List<UpdateProductStockDto> items, CancellationToken cancellationToken)
        {
            foreach (var item in items)
            {
                var product = await dbContext.Products
                    .FirstOrDefaultAsync(
                        p => p.Id == item.ProductId,
                        cancellationToken
                    );

                if (product != null)
                {
                    if (movementType == ProductMovementEnum.INVOICE)
                    {
                        product.Stock -= item.Quantity;
                    }
                    else if (movementType == ProductMovementEnum.PURCHASE)
                    {
                        product.Stock += item.Quantity;
                    }
                    else
                    {
                        throw new InvalidOperationException("Invalid product movement type.");
                    }
                }
            }
        }

        public async Task<bool> ProductExistsInInvoicesAsync(int companyId, int productId, CancellationToken cancellationToken)
        {
            return await dbContext.Invoice
                .AsNoTracking()
                .Where(i => i.CompanyId == companyId)
                .SelectMany(i => i.InvoiceItems)
                .AnyAsync(ii => ii.ProductId == productId, cancellationToken);
        }

        public async Task<bool> ProductExistsInPurchasesAsync(int companyId, int productId, CancellationToken cancellationToken)
        {
            return await dbContext.Purchase
                .AsNoTracking()
                .Where(i => i.CompanyId == companyId)
                .SelectMany(i => i.PurchaseItems)
                .AnyAsync(ii => ii.ProductId == productId, cancellationToken);
        }
    }
}
