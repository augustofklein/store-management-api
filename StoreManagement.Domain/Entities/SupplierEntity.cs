namespace StoreManagement.Domain.Entities
{
    public class SupplierEntity
    {
        public int CompanyId { get; set; }
        public int Id { get; set; }
        public string Identification { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<PurchaseEntity> Purchases { get; set; } = [];
    }
}
