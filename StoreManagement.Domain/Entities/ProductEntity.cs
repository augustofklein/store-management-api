using StoreManagement.Domain.Entities;

namespace StoreManagement.Infrastructure.DBContext.Model
{
    public class ProductEntity
    {
        public int CompanyId { get; set; }
        public int Id { get; set; }
        public string SkuId { get; set; } = null!;
        public bool Status { get; set; }
        public string Barcode { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Stock { get; set; }

        public virtual CompanyEntity Company { get; set; } = null!;
        public ProductPriceEntity ProductPrice { get; set; } = null!;
        public ICollection<InvoiceItemEntity> InvoiceItems { get; set; } = [];
        public ICollection<PurchaseItemEntity> PurchaseItems { get; set; } = [];
        public ICollection<ProductMovementEntity> ProductMovements { get; set; } = [];
    }
}
