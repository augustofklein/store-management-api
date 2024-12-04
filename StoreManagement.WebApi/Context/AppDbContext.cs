using Microsoft.EntityFrameworkCore;

namespace StoreManagement.WebApi.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) { }
}
