namespace StoreManagement.Application.Purchase.Model
{
    public class AddPurchaseDocumentDto
    {
        public string DocumentNumber { get; set; } = string.Empty;
        public string DocumentSerie { get; set; } = string.Empty;
        public string DocumentMod { get; set; } = string.Empty;
        public string DocumentKey { get; set; } = string.Empty;
        public DateTimeOffset DocumentDate { get; set; }
    }
}
