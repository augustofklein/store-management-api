using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Infrastructure.DBContext.Model;

namespace StoreManagement.Infrastructure.DBContext.EntityFramework.Configurations
{
    public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.ToTable("products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.CompanyId)
                .HasColumnName("company_id")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(p => p.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.SkuId)
                .HasColumnName("sku_id")
                .HasColumnType("varchar(20)")
                .IsRequired();

            builder.Property(p => p.Status)
                .HasColumnName("status")
                .HasColumnType("boolean")
                .IsRequired();

            builder.Property(p => p.Barcode)
                .HasColumnName("barcode")
                .HasColumnType("varchar(13)")
                .IsRequired();

            builder.Property(p => p.Description)
                .HasColumnName("description")
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(p => p.Stock)
                .HasColumnName("stock")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(pp => pp.AverageCost)
                .HasColumnName("average_cost")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.HasOne(p => p.Company)
               .WithMany(p => p.Products)
               .HasForeignKey(p => p.CompanyId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(p => p.CompanyId).HasDatabaseName("idx_products_company_id");
            builder.HasIndex(p => new { p.CompanyId, p.SkuId }).IsUnique();
            builder.HasIndex(p => new { p.CompanyId, p.Barcode }).IsUnique();
        }
    }
}
