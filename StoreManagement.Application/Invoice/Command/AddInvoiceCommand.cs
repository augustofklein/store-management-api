using CSharpFunctionalExtensions;
using MediatR;
using StoreManagement.Application.Invoice.Model;

namespace StoreManagement.Application.Invoice.Command
{
    public class AddInvoiceCommand(int customerId, List<AddInvoiceItemsDto> invoiceItems) : IRequest<Result>
    {
        public int CompanyId { get; set; }
        public int CustomerId { get; set; } = customerId;
        public DateTime InvoiceDate { get; set; } = DateTime.Now;

        public List<AddInvoiceItemsDto> InvoiceItems { get; set; } = invoiceItems;
    }
}
