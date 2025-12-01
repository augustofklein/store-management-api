namespace StoreManagement.Infrastructure.DBContext.Model
{
    public class CustomerEntity
    {
        public int CompanyId { get; set; }
        public int Id { get; set; }
        public string Identification { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public virtual CompanyEntity Company { get; set; }
        public virtual ICollection<CustomerContactEntity> CustomerContacts { get; set; } = [];
    }
}
