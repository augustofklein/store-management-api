using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Application.Supplier.Model;
using StoreManagement.Domain.Entities;
using StoreManagement.Infrastructure.DBContext;

namespace StoreManagement.Infrastructure.Repository.Supplier
{
    public class SupplierRepository(AppDbContext dbContext) : ISupplierRepository
    {
        public async Task<Result<IEnumerable<SupplierDto>>> ReturnAllSuppliersAsync(int companyId, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await dbContext.Supplier
                .AsNoTracking()
                .Where(i => i.CompanyId == companyId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(i => new SupplierDto
                {
                    Id = i.Id,
                    DocumentNumber = i.DocumentNumber,
                    Name = i.Name,
                }).ToListAsync(cancellationToken);
        }

        public async Task<bool> ValidatePurchaseLinkedAsync(int companyId, int supplierId, CancellationToken cancellationToken)
        {
            return await dbContext.Purchase
                .AsNoTracking()
                .Where(i => i.CompanyId == companyId && i.SupplierId == supplierId)
                .FirstOrDefaultAsync(cancellationToken) != null;
        }

        public async Task<Result> DeleteSupplierAsync(int companyId, int supplierId, CancellationToken cancellationToken)
        {
            var supplier = await dbContext.Supplier
                .Where(i => i.CompanyId == companyId && i.Id == supplierId)
                .FirstOrDefaultAsync(cancellationToken);

            if (supplier == null)
                return Result.Failure($"Supplier with ID {supplierId} not found.");

            dbContext.Supplier.Remove(supplier);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<bool> ValidateSupplierExistsByIdAsync(int companyId, int supplierId, CancellationToken cancellationToken)
        {
            return await dbContext.Supplier
                .AsNoTracking()
                .Where(i => i.CompanyId == companyId && i.Id == supplierId)
                .FirstOrDefaultAsync(cancellationToken) != null;
        }

        public async Task<Result> AddSupplierAsync(int companyId, AddSupplierDto addSupplier, CancellationToken cancellationToken)
        {
            var supplier = new SupplierEntity
            {
                CompanyId = companyId,
                DocumentNumber = addSupplier.DocumentNumber,
                Name = addSupplier.Name
            };

            await dbContext.Supplier.AddAsync(supplier, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<bool> ValidateSupplierExistsByDocumentNumberAsync(int companyId, string documentNumber, CancellationToken cancellationToken)
        {
            return await dbContext.Supplier
                .AsNoTracking()
                .Where(i => i.CompanyId == companyId && i.DocumentNumber == documentNumber)
                .FirstOrDefaultAsync(cancellationToken) != null;
        }

        public async Task<int> ReturnSupplierIdByDocumentNumber(int companyId, string documentNumber, CancellationToken cancellationToken)
        {
            return await dbContext.Supplier
                .AsNoTracking()
                .Where(i => i.CompanyId == companyId && i.DocumentNumber == documentNumber)
                .Select(i => i.Id)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
