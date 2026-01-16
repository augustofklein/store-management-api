namespace StoreManagement.Application.Purchase.Model
{
    public class PurchaseDto
    {
        public int PurchaseId { get; set; }
        public DateTimeOffset PurchaseDate { get; set; }
        public DateTimeOffset PurchaseEntryDate { get; set; }
        public PurchaseDocument Document { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public SupplierPurchase Supplier { get; set; } = null!;
        public List<PurchaseItem> Items { get; set; } = [];

        public class PurchaseDocument
        {
            public string DocumentNumber { get; set; } = string.Empty;
            public string DocumentSerie { get; set; } = string.Empty;
            public string DocumentMod { get; set; } = string.Empty;
            public string DocumentKey { get; set; } = string.Empty;
            public DateTimeOffset DocumentDate { get; set; }
        }

        public class PurchaseItem
        {
            public int ProductId { get; set; }
            public string SkuId { get; set; } = string.Empty;
            public string Barcode { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public int Quantity { get; set; }
            public int Package { get; set; }
        }

        public class SupplierPurchase
        {
            public int Id { get; set; }
            public string DocumentNumber { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
        }
    }
}
