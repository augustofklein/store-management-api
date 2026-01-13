namespace StoreManagement.Application.Purchase.Command
{
    public class AddPurchaseCommand
    {
        public int CompanyId { get; set; }
        public PurchaseDocument Document { get; set; } = null!;
        public List<PurchaseProduct> Products { get; set; } = [];

        public class PurchaseDocument
        {
            public string Number { get; set; } = string.Empty;
            public string Serie { get; set; } = string.Empty;
            public string Mod { get; set; } = string.Empty;
            public string Key { get; set; } = string.Empty;
        }

        public class PurchaseProduct
        {
            public int Id { get; set; }
            public decimal Price { get; set; }
            public int Package { get; set; }
            public decimal Quantity { get; set; }
        }
    }
}
