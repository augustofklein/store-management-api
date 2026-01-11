using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Infrastructure.DBContext;

namespace StoreManagement.Infrastructure.Repository.Companie
{
    public class CompanyRepository(AppDbContext dbContext) : ICompanyRepository
    {
        public async Task<bool> ExistsCompanyByDocumentNumberAsync(int companyId, string documentNumber, CancellationToken cancellationToken)
        {
            return await dbContext.Companies
                .AsNoTracking()
                .Where(i => i.Id == companyId && i.DocumentNumber == documentNumber)
                .FirstOrDefaultAsync(cancellationToken) != null;
        }
    }
}
