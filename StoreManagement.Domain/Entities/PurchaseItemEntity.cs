using StoreManagement.Infrastructure.DBContext.Model;

namespace StoreManagement.Domain.Entities
{
    public class PurchaseItemEntity
    {
        public int PurchaseId { get; set; }
        public int ProductId { get; set; }
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Package { get; set; }
        public int Quantity { get; set; }

        public virtual PurchaseEntity Purchase { get; set; } = null!;
        public virtual ProductEntity Product { get; set; } = null!;
    }
}
