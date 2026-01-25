using StoreManagement.Infrastructure.DBContext.Model;

namespace StoreManagement.Domain.Entities
{
    public class SupplierEntity
    {
        public int CompanyId { get; set; }
        public int Id { get; set; }
        public string DocumentNumber { get; set; } = null!;
        public string Name { get; set; } = null!;

        public virtual CompanyEntity Company { get; set; } = null!;
        public virtual ICollection<PurchaseEntity> Purchases { get; set; } = [];
    }
}
