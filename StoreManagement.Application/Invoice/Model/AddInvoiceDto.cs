namespace StoreManagement.Application.Invoice.Model
{
    public class AddInvoiceDto
    {
        public int CompanyId { get; set; }
        public int CustomerId { get; set; }
        public DateTime InvoiceDate { get; set; }

        public List<AddInvoiceItemsDto> InvoiceItems { get; set; } = [];
    }
}
