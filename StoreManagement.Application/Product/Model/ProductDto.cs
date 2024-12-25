namespace StoreManagement.Application.Product.Model
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }

        public ProductDto(int id, string description, int stock)
        {
            Id = id;
            Description = description;
            Stock = stock;
        }

        public ProductDto()
        {
            Id = default;
            Description = string.Empty;
            Stock = default;
        }
    }
}
