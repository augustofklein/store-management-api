using System.ComponentModel.DataAnnotations;

namespace StoreManagement.WebApi.InputModel.Product
{
    public class AddProductInputModel
    {
        [Required]
        public string SkuId { get; set; } = string.Empty;

        [Required]
        public bool Status { get; set; }

        [Required]
        public string Barcode { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        public int Stock { get; set; }
    }
}
