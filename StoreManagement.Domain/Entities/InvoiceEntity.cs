using StoreManagement.Infrastructure.DBContext.Model;

namespace StoreManagement.Domain.Entities
{
    public class InvoiceEntity
    {
        public int CompanyId { get; set; }
        public int CustomerId { get; set; }
        public int Id { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }

        public virtual CompanyEntity Company { get; set; } = null!;
        public virtual CustomerEntity Customer { get; set; } = null!;
        public ICollection<InvoiceItemEntity> InvoiceItems { get; set; } = [];
    }
}
