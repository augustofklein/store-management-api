using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Application.Product.Model;
using StoreManagement.Domain.Entities;
using StoreManagement.Domain.Enums;
using StoreManagement.Infrastructure.DBContext;
using StoreManagement.Infrastructure.DBContext.Model;
using System.ComponentModel.Design;

namespace StoreManagement.Infrastructure.Repository.Product
{
    public class ProductRepository(AppDbContext dbContext, IEFTransactionManager eFTransactionManager) : IProductRepository
    {
        public async Task<Result> AddProductAsync(int companyId, string skuId, bool status, string barcode, string description, int stock, decimal price, CancellationToken cancellationToken)
        {
            await eFTransactionManager.BeginAsync(cancellationToken);

            try
            {
                var product = new ProductEntity
                {
                    CompanyId = companyId,
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

                var productMovement = new ProductMovementEntity
                {
                    Product = product,
                    MovementType = ProductMovementEnum.INITIAL,
                    Quantity = stock,
                    Price = price,
                    CreatedAt = DateTimeOffset.UtcNow
                };

                await dbContext.Products.AddAsync(product, cancellationToken);
                await dbContext.ProductMovements.AddAsync(productMovement, cancellationToken);
                await eFTransactionManager.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await eFTransactionManager.RollbackAsync(cancellationToken);
                return Result.Failure($"An error occurred while adding the product: {ex.Message}");
            }

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

        public async Task<Result> EditProductAsync(int companyId, int id, bool status, string description, decimal price, CancellationToken cancellationToken)
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

            var productPrice = await dbContext.ProductPrice
                .FirstOrDefaultAsync(pp => pp.ProductId == product.Id, cancellationToken);

            if (productPrice == null)
            {
                return Result.Failure($"Product price with ID {id} not found.");
            }

            product.Description = description;
            product.Status = status;
            productPrice.Price = price;

            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result<ProductPagedResultDto>> GetProductsAsync(int companyId, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var query = dbContext.Products
                .Where(p => p.CompanyId == companyId);

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderBy(p => p.Id)
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

            var totalPages = pageSize <= 0 ? 0 : (int)Math.Ceiling((double)totalCount / pageSize);

            var result = new ProductPagedResultDto
            {
                Items = items,
                TotalCount = totalCount,
                TotalPages = totalPages,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return Result.Success(result);
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
                .Where(p => p.CompanyId == companyId && barcodeProducts.Contains(p.Barcode))
                .Select(p => p.Barcode)
                .ToListAsync(cancellationToken);

            var missingProductIds = barcodeProducts.Except(existingProductIds).ToList();

            if (missingProductIds.Count != 0)
                return Result.Failure($"The following barcode products do not exist: {string.Join(", ", missingProductIds)}");

            return Result.Success();
        }

        public async Task AddProductMovementArrayAsync(ProductMovementEnum movementType, List<AddProductMovementDto> items, CancellationToken cancellationToken)
        {
            var productMovements = items.Select(ii => new ProductMovementEntity
            {
                ProductId = ii.ProductId,
                MovementType = movementType,
                Quantity = ii.Quantity,
                Price = ii.Price,
                CreatedAt = DateTimeOffset.UtcNow
            });

            await dbContext.ProductMovements.AddRangeAsync(productMovements, cancellationToken);
        }

        public async Task<Result> UpdateProductStockArrayAsync(int companyId, ProductMovementEnum movementType, List<ProductStockDto> items, CancellationToken cancellationToken)
        {
            var productIds = items
                .Select(i => i.ProductId)
                .Distinct()
                .ToList();

            var products = await dbContext.Products
                .Where(p => p.CompanyId == companyId && productIds.Contains(p.Id))
                .ToDictionaryAsync(p => p.Id, cancellationToken);

            foreach (var item in items)
            {
                if (!products.TryGetValue(item.ProductId, out var product))
                    return Result.Failure($"Product with ID {item.ProductId} not found.");

                switch (movementType)
                {
                    case ProductMovementEnum.INVOICE:
                        product.Stock -= item.Quantity;
                        break;

                    case ProductMovementEnum.PURCHASE:
                        product.Stock += item.Quantity;
                        break;

                    default:
                        return Result.Failure("Invalid product movement type.");
                }
            }

            return Result.Success();
        }

        public async Task<bool> ProductExistsInInvoicesAsync(int companyId, int productId, CancellationToken cancellationToken)
        {
            return await dbContext.Invoice
                .Where(i => i.CompanyId == companyId)
                .SelectMany(i => i.InvoiceItems)
                .AnyAsync(ii => ii.ProductId == productId, cancellationToken);
        }

        public async Task<bool> ProductExistsInPurchasesAsync(int companyId, int productId, CancellationToken cancellationToken)
        {
            return await dbContext.Purchase
                .Where(i => i.CompanyId == companyId)
                .SelectMany(i => i.PurchaseItems)
                .AnyAsync(ii => ii.ProductId == productId, cancellationToken);
        }

        public async Task<IEnumerable<ProductDto>> ReturnProductsByBarcodeAsync(int companyId, List<string> barcodes, CancellationToken cancellationToken)
        {
            return await dbContext.Products
                .Where(p => p.CompanyId == companyId && barcodes.Contains(p.Barcode))
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

        public async Task<Result> UpdateProductAverageCostArrayAsync(int companyId, List<AddProductMovementDto> items, CancellationToken cancellationToken)
        {
            var productIds = items
                .Select(i => i.ProductId)
                .Distinct()
                .ToList();

            var products = await dbContext.Products
                .Where(p => p.CompanyId == companyId && productIds.Contains(p.Id))
                .ToDictionaryAsync(p => p.Id, cancellationToken);

            foreach (var item in items)
            {
                if (!products.TryGetValue(item.ProductId, out var product))
                    return Result.Failure($"Product with ID {item.ProductId} not found.");

                if (product.Stock == 0)
                {
                    product.AverageCost = item.Price;
                }
                else
                {
                    var totalCurrentValue =
                        product.Stock * product.AverageCost;

                    var totalPurchaseValue =
                        item.Quantity * item.Price;

                    var newStockQuantity =
                        product.Stock + item.Quantity;

                    product.AverageCost =
                        (totalCurrentValue + totalPurchaseValue) / newStockQuantity;
                }
            }

            return Result.Success();
        }

        public async Task<Result> ValidateInvoiceProductsStockAsync(int companyId, List<ProductStockDto> items, CancellationToken cancellationToken)
        {
            if (items == null)
                return Result.Success();

            var productIds = items
                .Select(i => i.ProductId)
                .Distinct()
                .ToList();

            if (!productIds.Any())
                return Result.Success();

            var products = await dbContext.Products
                .Where(p => p.CompanyId == companyId && productIds.Contains(p.Id))
                .Select(p => new { p.Id, p.Stock, p.SkuId })
                .ToDictionaryAsync(p => p.Id, cancellationToken);

            var missingProductIds = productIds.Except(products.Keys).ToList();
            if (missingProductIds.Count != 0)
                return Result.Failure($"The following product IDs do not exist: {string.Join(", ", missingProductIds)}");

            var insufficient = new List<string>();
            foreach (var item in items)
            {
                if (!products.TryGetValue(item.ProductId, out var product))
                {
                    insufficient.Add($"ProductId {item.ProductId} not found");
                    continue;
                }

                if (product.Stock < item.Quantity)
                {
                    insufficient.Add($"ProductId {product.Id} (SKU: {product.SkuId}) - Available: {product.Stock}, Required: {item.Quantity}");
                }
            }

            if (insufficient.Count != 0)
                return Result.Failure($"Insufficient stock for: {string.Join("; ", insufficient)}");

            return Result.Success();
        }
    }
}
