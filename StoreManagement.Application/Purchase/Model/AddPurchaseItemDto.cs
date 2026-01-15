namespace StoreManagement.Application.Purchase.Model
{
    public class AddPurchaseItemDto
    {
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Package { get; set; }
        public int Quantity { get; set; }
        public decimal ShippingCost { get; set; }
    }
}
