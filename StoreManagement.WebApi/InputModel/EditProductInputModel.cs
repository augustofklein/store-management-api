using System.ComponentModel.DataAnnotations;

namespace StoreManagement.WebApi.InputModel
{
    public class EditProductInputModel
    {
        [Required]
        public bool Status { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;
    }
}
