namespace StoreManagement.Infrastructure.DBContext.Model
{
    public class ContactTypeEntity
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;

        public virtual ICollection<CustomerContactEntity> CustomerContacts { get; set; } = [];
    }
}
