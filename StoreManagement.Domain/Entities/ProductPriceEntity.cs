using StoreManagement.Infrastructure.DBContext.Model;

namespace StoreManagement.Domain.Entities
{
    public class ProductPriceEntity
    {
        public int ProductId { get; set; }
        public int Id { get; set; }
        public decimal Price { get; set; }

        public virtual ProductEntity Product { get; set; } = null!;
    }
}
