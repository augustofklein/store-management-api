using StoreManagement.Infrastructure.DBContext.Model;

namespace StoreManagement.Domain.Entities
{
    public class PurchaseEntity
    {
        public int CompanyId { get; set; }
        public int SupplierId { get; set; }
        public int Id { get; set; }
        public string DocumentNumber { get; set; } = null!;
        public string DocumentSerie { get; set; } = null!;
        public string DocumentMod { get; set; } = null!;
        public string DocumentKey { get; set; } = null!;
        public DateTimeOffset PurchaseDate { get; set; }
        public DateTimeOffset PurchaseEntryDate { get; set; }
        public decimal TotalAmount { get; set; }

        public virtual CompanyEntity Company { get; set; } = null!;
        public virtual SupplierEntity Supplier { get; set; } = null!;
        public virtual ICollection<PurchaseItemEntity> PurchaseItems { get; set; } = [];
    }
}
