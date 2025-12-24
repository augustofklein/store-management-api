using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Application.Product.Model;
using StoreManagement.Domain.Entities;
using StoreManagement.Infrastructure.DBContext;
using StoreManagement.Infrastructure.DBContext.Model;

namespace StoreManagement.Infrastructure.Repository.Product
{
    public class ProductRepository(AppDbContext dbContext, IMapper mapper) : IProductRepository
    {
        public async Task<Result> AddProduct(int companyId, string skuId, bool status, string barcode, string description, int stock, decimal price, CancellationToken cancellationToken)
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

        public async Task<Result> RemoveProduct(int companyId, int id, CancellationToken cancellationToken)
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

        public async Task<Result> EditProduct(int companyId, int id, bool status, string description, CancellationToken cancellationToken)
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

        public async Task<Result<IEnumerable<ProductDto>>> GetProducts(int companyId, int pageNumber, int pageSize, CancellationToken cancellationToken)
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
    }
}
