namespace StoreManagement.Infrastructure.DBContext.Model
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }

        public ProductEntity(int id, string description, int stock)
        {
            Id = id;
            Description = description;
            Stock = stock;
        }

        public ProductEntity()
        {
            Id = default;
            Description = string.Empty;
            Stock = default;
        }
    }
}
