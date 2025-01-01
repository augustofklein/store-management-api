namespace StoreManagement.Infrastructure.DBContext.Model
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }

        public ProductEntity(int id, string barcode, string description, int stock)
        {
            Id = id;
            Barcode = barcode;
            Description = description;
            Stock = stock;
        }

        public ProductEntity()
        {
            Id = default;
            Barcode = string.Empty;
            Description = string.Empty;
            Stock = default;
        }
    }
}
