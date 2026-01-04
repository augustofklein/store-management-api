using StoreManagement.Domain.Entities;

namespace StoreManagement.Infrastructure.DBContext.Model
{
    public class ProductEntity
    {
        public int CompanyId { get; set; }
        public int Id { get; set; }
        public string SkuId { get; set; } = string.Empty;
        public bool Status { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Stock { get; set; }

        public virtual CompanyEntity Company { get; set; } = null!;
        public ProductPriceEntity ProductPrice { get; set; } = null!;
        public ICollection<InvoiceItemEntity> InvoiceItems { get; set; } = [];
        public ICollection<PurchaseItemEntity> PurchaseItems { get; set; } = [];
        public ICollection<ProductMovementEntity> ProductMovements { get; set; } = [];
    }
}
