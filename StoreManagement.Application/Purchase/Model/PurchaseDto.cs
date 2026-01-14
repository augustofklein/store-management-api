namespace StoreManagement.Application.Purchase.Model
{
    public class PurchaseDto
    {
        public int PurchaseId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal TotalAmount { get; set; }
        public required SupplierPurchase Supplier {  get; set; }
        public List<PurchaseItem> Items { get; set; } = [];

        public class PurchaseItem
        {
            public int ProductId { get; set; }
            public string SkuId { get; set; } = string.Empty;
            public string Barcode { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public int Package { get; set; }
            public int Quantity { get; set; }
        }

        public class SupplierPurchase
        {
            public int Id { get; set; }
            public string DocumentNumber { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
        }
    }
}
