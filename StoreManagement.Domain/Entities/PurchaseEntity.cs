using StoreManagement.Infrastructure.DBContext.Model;

namespace StoreManagement.Domain.Entities
{
    public class PurchaseEntity
    {
        public int CompanyId { get; set; }
        public int SupplierId { get; set; }
        public int Id { get; set; }
        public string DocumentKey { get; set; } = string.Empty;
        public DateTime PurchaseDate { get; set; }
        public decimal TotalAmount { get; set; }

        public virtual CompanyEntity Company { get; set; } = null!;
        public virtual SupplierEntity Supplier { get; set; } = null!;
        public virtual ICollection<PurchaseItemEntity> PurchaseItems { get; set; } = [];
    }
}
