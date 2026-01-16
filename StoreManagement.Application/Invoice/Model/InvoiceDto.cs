namespace StoreManagement.Application.Invoice.Model
{
    public class InvoiceDto
    {
        public int InvoiceId { get; set; }
        public DateTimeOffset InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public required CustomerInvoice Customer { get; set; }
        public List<InvoiceItem> Items { get; set; } = [];

        public class InvoiceItem
        {
            public int ProductId { get; set; }
            public string SkuId { get; set; } = string.Empty;
            public string Barcode { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public int Quantity { get; set; }
        }

        public class CustomerInvoice
        {
            public int Id { get; set; }
            public string DocumentNumber { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string Address { get; set; } = string.Empty;
        }
    }
}
