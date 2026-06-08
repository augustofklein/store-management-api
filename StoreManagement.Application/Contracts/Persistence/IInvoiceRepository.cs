using CSharpFunctionalExtensions;
using StoreManagement.Application.Invoice.Model;

namespace StoreManagement.Application.Contracts.Persistence
{
    public interface IInvoiceRepository
    {
        Task<Result<IEnumerable<InvoiceDto>>> ReturnAllInvoicesAsync(int companyId, int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task AddInvoiceAsync(AddInvoiceDto invoice, CancellationToken cancellationToken);
        Task<bool> VerifyPaymentsIdExistAsync(int companyId, IEnumerable<int> paymentsId, CancellationToken cancellationToken);
    }
}
