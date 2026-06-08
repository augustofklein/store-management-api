using CSharpFunctionalExtensions;
using MediatR;
using StoreManagement.Application.Invoice.Model;
using StoreManagement.Application.Payments.Model;

namespace StoreManagement.Application.Invoice.Command
{
    public class AddInvoiceCommand(int customerId, List<AddInvoiceItemsDto> invoiceItems, List<AddInvoicePaymentDto> payments) : IRequest<Result>
    {
        public int CompanyId { get; set; }
        public int CustomerId { get; set; } = customerId;

        public List<AddInvoiceItemsDto> InvoiceItems { get; set; } = invoiceItems;
        public List<AddInvoicePaymentDto> Payments { get; set; } = payments;
    }
}
