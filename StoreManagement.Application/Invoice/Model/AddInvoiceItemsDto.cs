namespace StoreManagement.Application.Invoice.Model
{
    public class AddInvoiceItemsDto
    {
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
