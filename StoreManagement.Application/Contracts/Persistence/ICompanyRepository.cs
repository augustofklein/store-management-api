namespace StoreManagement.Application.Contracts.Persistence
{
    public interface ICompanyRepository
    {
        Task<bool> ExistsCompanyByDocumentNumberAsync(int companyId, string documentNumber, CancellationToken cancellationToken);
    }
}
