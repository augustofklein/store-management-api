namespace StoreManagement.Application.Product.Model
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }

        public ProductDto(int id, string barcode, string description, int stock)
        {
            Id = id;
            Barcode = barcode;
            Description = description;
            Stock = stock;
        }

        public ProductDto()
        {
            Id = default;
            Barcode = string.Empty;
            Description = string.Empty;
            Stock = default;
        }
    }
}
