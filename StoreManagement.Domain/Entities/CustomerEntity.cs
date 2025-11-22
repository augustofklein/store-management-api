namespace StoreManagement.Infrastructure.DBContext.Model
{
    public class CustomerEntity
    {
        public int Id { get; set; }
        public string Identification { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
