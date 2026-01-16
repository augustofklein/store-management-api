using CSharpFunctionalExtensions;
using MediatR;
using StoreManagement.Application.Invoice.Model;

namespace StoreManagement.Application.Invoice.Command
{
    public class AddInvoiceCommand(int customerId, List<AddInvoiceItemsDto> invoiceItems) : IRequest<Result>
    {
        public int CompanyId { get; set; }
        public int CustomerId { get; set; } = customerId;
        public DateTimeOffset InvoiceDate { get; set; } = DateTimeOffset.Now.DateTime;

        public List<AddInvoiceItemsDto> InvoiceItems { get; set; } = invoiceItems;
    }
}
