using Microsoft.EntityFrameworkCore;
using StoreManagement.Domain.Entities;
using StoreManagement.Infrastructure.DBContext.EntityFramework.Configurations;
using StoreManagement.Infrastructure.DBContext.Model;

namespace StoreManagement.Infrastructure.DBContext
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public required DbSet<UserEntity> Users { get; set; }
        public required DbSet<ProductEntity> Products { get; set; }
        public required DbSet<ProductPriceEntity> ProductPrice { get; set; }
        public required DbSet<CustomerEntity> Customers { get; set; }
        public required DbSet<CustomerContactEntity> CustomerContacts { get; set; }
        public required DbSet<ContactTypeEntity> ContactType { get; set; }
        public required DbSet<InvoiceEntity> Invoice { get; set; }
        public required DbSet<InvoiceItemEntity> InvoiceItems { get; set; }
        public required DbSet<PurchaseEntity> Purchase { get; set; }
        public required DbSet<PurchaseItemEntity> PurchaseItems { get; set; }
        public required DbSet<SupplierEntity> Supplier { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductPriceTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerContactTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ContactTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceItemsTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PurchaseTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PurchaseItemsTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierTypeConfiguration());
        }
    }
}
