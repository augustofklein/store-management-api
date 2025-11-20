namespace StoreManagement.Application.Product.Model
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string SkuId { get; set; }
        public bool Status { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }

        public ProductDto(int id, string skuId, bool status, string barcode, string description, int stock)
        {
            Id = id;
            SkuId = skuId;
            Status = status;
            Barcode = barcode;
            Description = description;
            Stock = stock;
        }

        public ProductDto()
        {
            Id = default;
            SkuId = string.Empty;
            Status = false;
            Barcode = string.Empty;
            Description = string.Empty;
            Stock = default;
        }
    }
}
