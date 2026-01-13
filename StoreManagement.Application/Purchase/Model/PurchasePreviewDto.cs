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
            public string Number { get; set; } = string.Empty;
            public string Serie { get; set; } = string.Empty;
            public string Mod { get; set; } = string.Empty;
            public string Key { get; set; } = string.Empty;
            public int Status { get; set; }
        }

        public class StorePreviewDto
        {
            public string DocumentNumber { get; set; } = string.Empty;
        }

        public class SupplierPreviewDto
        {
            public string DocumentNumber { get; set; } = string.Empty;
        }

        public class ProductPreviewDto
        {
            public int? Id { get; set; }
            public string SkuId { get; set; } = string.Empty;
            public string Barcode { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public int Package { get; set; }
            public decimal Quantity { get; set; }
            public decimal ShippingCost { get; set; }
            public decimal Total => ((Quantity * Package) * Price) + ShippingCost;
            public bool ProductFound => Id.HasValue;
            public string? ValidationMessage { get; set; }
        }
    }
}
