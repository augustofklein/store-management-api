using System.ComponentModel.DataAnnotations;

namespace StoreManagement.WebApi.Model
{
    public class User
    {
        [Required]
        public string Username { get; set;} = string.Empty;
        
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
