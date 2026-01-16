namespace StoreManagement.Application.Purchase.Model
{
    public class PurchasePreviewDto
    {
        public string StoreDocumentNumber { get; set; } = string.Empty;
        public SupplierPreviewDto SupplierInformation { get; set; } = null!;
        public DocumentPreviewDto FiscalDocument { get; set; } = null!;
        public List<ProductPreviewDto> Products { get; set; } = [];
        public decimal TotalAmount { get; set; }

        public class SupplierPreviewDto
        {
            public int SupplierId { get; set; }
            public string DocumentNumber { get; set; } = string.Empty;
        }

        public class DocumentPreviewDto
        {
            public string DocumentNumber { get; set; } = string.Empty;
            public string DocumentSerie { get; set; } = string.Empty;
            public string DocumentMod { get; set; } = string.Empty;
            public string DocumentKey { get; set; } = string.Empty;
            public DateTimeOffset DocumentDate { get; set; }
            public int DocumentStatus { get; set; }
        }

        public class ProductPreviewDto : PurchaseItemFiscalDocumentDto
        {
            public int? Id { get; set; }
            public string SkuId { get; set; } = string.Empty;
            public string Barcode { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public decimal Total => ((Quantity * Package) * Price) + ShippingCost;
            public bool ProductFound => Id.HasValue;
            public string? ValidationMessage { get; set; }
        }
    }
}
