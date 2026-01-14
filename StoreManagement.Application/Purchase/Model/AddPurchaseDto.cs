namespace StoreManagement.Application.Purchase.Model
{
    public class AddPurchaseDto
    {
        public DateTimeOffset PurchaseEntryDate { get; set; }
        public int SupplierId { get; set; }
        public AddPurchaseDocumentDto Document { get; set; } = null!;
        public List<AddPurchaseItemDto> Products { get; set; } = null!;
    }
}
