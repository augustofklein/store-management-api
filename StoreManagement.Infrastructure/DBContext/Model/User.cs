using System;
using System.ComponentModel.DataAnnotations;

namespace StoreManagement.Infrastructure.DBContext.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
