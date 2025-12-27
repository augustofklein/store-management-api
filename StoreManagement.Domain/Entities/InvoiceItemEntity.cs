using StoreManagement.Infrastructure.DBContext.Model;

namespace StoreManagement.Domain.Entities
{
    public class InvoiceItemEntity
    {
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public virtual InvoiceEntity Invoice { get; set; } = null!;
        public virtual ProductEntity Product { get; set; } = null!;
    }
}
