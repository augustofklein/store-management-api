using System.ComponentModel.DataAnnotations;

namespace StoreManagement.WebApi.Model
{
    public class AddProductInputModel
    {
        [Required]
        public int Id { get; set; } = default;

        [Required]
        public string Barcode { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        public int Stock { get; set; } = default;
    }
}
