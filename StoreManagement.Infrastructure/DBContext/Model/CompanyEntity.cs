namespace StoreManagement.Infrastructure.DBContext.Model
{
    public class CompanyEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public required ICollection<ProductEntity> Products { get; set; }
    }
}
