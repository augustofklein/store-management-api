using CSharpFunctionalExtensions;
using StoreManagement.Application.Invoice.Command;

namespace StoreManagement.Application.Invoice.Service
{
    public interface IInvoiceService
    {
        Task<Result> ValidateInvoiceProductsExists(AddInvoiceCommand command, CancellationToken cancellationToken);
    }
}
