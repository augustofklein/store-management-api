using System.ComponentModel.DataAnnotations;

namespace StoreManagement.WebApi.Model
{
    public class EditProductInputModel
    {
        [Required]
        public string Description { get; set; } = string.Empty;
    }
}
