using System;
using System.ComponentModel.DataAnnotations;

namespace StoreManagement.WebApi.Model
{
    public class AddProduct
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }
        
        [Required]
        public int Stock { get; set; }
    }
}
