using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Product.Model;
using StoreManagement.Infrastructure.DBContext;
using StoreManagement.Infrastructure.DBContext.Model;

namespace StoreManagement.Infrastructure.Repository.Product
{
    public class ProductRepository(AppDbContext dbContext) : IProductRepository
    {
        public async Task<Result> AddProduct(int companyId, string skuId, bool status, string barcode, string description, int stock, CancellationToken cancellationToken)
        {
            var product = new ProductEntity
            {
                CompanyId = companyId,
                Id = 0,
                SkuId = skuId.Trim(),
                Status = status,
                Barcode = barcode,
                Description = description,
                Stock = stock
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

        public async Task<Result<IEnumerable<ProductDto>>> GetProducts(int companyId, CancellationToken cancellationToken)
        {
            var result = await dbContext.Products
                .Where(p => p.CompanyId == companyId)
                .ToListAsync(cancellationToken);

            if (result.Count == 0)
                return new Result<IEnumerable<ProductDto>>();

            // TODO: Verificar para melhorar o processo DE PARA
            var products = result.Select(r => new ProductDto
            {
                Id = r.Id,
                SkuId = r.SkuId.Trim(),
                Status = r.Status,
                Barcode = r.Barcode,
                Description = r.Description,
                Stock = r.Stock
            });

            return Result.Success(products);
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
