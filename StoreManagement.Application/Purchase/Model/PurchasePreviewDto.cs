namespace StoreManagement.Application.Purchase.Model
{
    public class PurchasePreviewDto
    {
        public DocumentPreviewDto FiscalDocument { get; set; } = null!;
        public StorePreviewDto Store { get; set; } = null!;
        public SupplierPreviewDto Supplier { get; set; } = null!;
        public List<ProductPreviewDto> Products { get; set; } = [];
        public decimal TotalAmount { get; set; }

        public class DocumentPreviewDto
        {
            public string DocumentKey { get; set; } = string.Empty;
            public int DocumentStatus { get; set; }
        }

        public class StorePreviewDto
        {
            public string Identification { get; set; } = string.Empty;
        }

        public class SupplierPreviewDto
        {
            public string Identification { get; set; } = string.Empty;
        }

        public class ProductPreviewDto
        {
            public string Barcode { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public decimal Quantity { get; set; }
            public decimal Price { get; set; }
            public decimal Total => Quantity * Price;
        }
    }
}
