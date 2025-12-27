namespace StoreManagement.Infrastructure.DBContext.Model
{
    public class CustomerContactEntity
    {
        public int CustomerId { get; set; }
        public int ContactTypeId { get; set; }
        public int Id { get; set; }
        public string Contact { get; set; } = string.Empty;

        public virtual CustomerEntity Customer { get; set; } = null!;
        public virtual ContactTypeEntity ContactType { get; set; } = null!;
    }
}
