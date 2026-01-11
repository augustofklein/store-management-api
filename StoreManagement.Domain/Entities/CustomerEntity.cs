using StoreManagement.Domain.Entities;

namespace StoreManagement.Infrastructure.DBContext.Model
{
    public class CustomerEntity
    {
        public int CompanyId { get; set; }
        public int Id { get; set; }
        public string DocumentNumber { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;

        public virtual CompanyEntity Company { get; set; } = null!;
        public virtual ICollection<CustomerContactEntity> CustomerContacts { get; set; } = [];
        public virtual ICollection<InvoiceEntity> Invoices { get; set; } = [];
    }
}
