using Microsoft.EntityFrameworkCore;
using StoreManagement.Domain.Entities;
using StoreManagement.Infrastructure.DBContext.EntityFramework.Configurations;
using StoreManagement.Infrastructure.DBContext.Model;

namespace StoreManagement.Infrastructure.DBContext
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public required DbSet<UserRoleEntity> UserRoles { get; set; }
        public required DbSet<UserEntity> Users { get; set; }
        public required DbSet<UserCompanyEntity> UserCompanies { get; set; }
        public required DbSet<CompanyEntity> Companies { get; set; }
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
        public required DbSet<ProductMovementEntity> ProductMovements { get; set; }
        public required DbSet<PaymentTypeEntity> PaymentTypes { get; set; }
        public required DbSet<IncoicePaymentsEntity> InvoicePayments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserRoleTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserCompanyTyperConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyTypeConfiguration());
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
            modelBuilder.ApplyConfiguration(new ProductMovementTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentTypeConfiguration());
            modelBuilder.ApplyConfiguration(new IncoicePaymentsConfiguration());
        }
    }
}
