using StoreManagement.Domain.Entities;

namespace StoreManagement.Infrastructure.DBContext.Model
{
    public class CompanyEntity
    {
        public int Id { get; set; }
        public string DocumentNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public required ICollection<UserEntity> Users { get; set; }
        public required ICollection<ProductEntity> Products { get; set; }
        public required ICollection<CustomerEntity> Customers { get; set; }
        public required ICollection<InvoiceEntity> Invoices { get; set; }
        public required ICollection<PurchaseEntity> Purchases { get; set; }
    }
}
