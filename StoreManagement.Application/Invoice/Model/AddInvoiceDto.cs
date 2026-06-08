using StoreManagement.Application.Payments.Model;

namespace StoreManagement.Application.Invoice.Model
{
    public class AddInvoiceDto
    {
        public int CompanyId { get; set; }
        public int CustomerId { get; set; }

        public List<AddInvoiceItemsDto> InvoiceItems { get; set; } = [];
        public List<AddInvoicePaymentDto> InvoicePayments { get; set; } = [];
    }
}
