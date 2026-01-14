namespace StoreManagement.Application.Purchase.Model
{
    public abstract class PurchaseItemFiscalDocumentDto
    {
        public decimal Price { get; set; }
        public int Package { get; set; }
        public int Quantity { get; set; }
        public decimal ShippingCost { get; set; }
    }
}
