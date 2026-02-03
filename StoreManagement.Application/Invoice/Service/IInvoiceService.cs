using CSharpFunctionalExtensions;
using StoreManagement.Application.Invoice.Command;

namespace StoreManagement.Application.Invoice.Service
{
    public interface IInvoiceService
    {
        Task<Result> ValidateAddInvoiceAsync(AddInvoiceCommand command, CancellationToken cancellationToken);
    }
}
