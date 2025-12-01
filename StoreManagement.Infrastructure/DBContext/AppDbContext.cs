using Microsoft.EntityFrameworkCore;
using StoreManagement.Infrastructure.DBContext.EntityFramework.Configurations;
using StoreManagement.Infrastructure.DBContext.Model;

namespace StoreManagement.Infrastructure.DBContext
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public required DbSet<UserEntity> Users { get; set; }
        public required DbSet<ProductEntity> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerContactTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ContactTypeConfiguration());
        }
    }
}
