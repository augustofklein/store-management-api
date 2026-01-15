using StoreManagement.Domain.Enums;
using StoreManagement.Infrastructure.DBContext.Model;

namespace StoreManagement.Domain.Entities
{
    public class ProductMovementEntity
    {
        public int ProductId { get; set; }
        public int Id { get; set; }
        public ProductMovementEnum MovementType { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public virtual ProductEntity Product { get; set; } = null!;
    }
}
