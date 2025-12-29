using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Application.Supplier.Model;
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
                    Identification = i.Identification,
                    Name = i.Name,
                }).ToListAsync(cancellationToken);
        }
    }
}
