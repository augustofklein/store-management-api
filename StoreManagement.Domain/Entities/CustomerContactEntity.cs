namespace StoreManagement.Infrastructure.DBContext.Model
{
    public class CustomerContactEntity
    {
        public int CustomerId { get; set; }
        public int ContactId { get; set; }
        public int Id { get; set; }
        public string Contact { get; set; } = string.Empty;

        public virtual CustomerEntity Customer { get; set; }
        public virtual ContactTypeEntity ContactType { get; set; }
    }
}
