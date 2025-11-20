using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Infrastructure.DBContext.Model;

namespace StoreManagement.Infrastructure.DBContext.EntityFramework.Configurations
{
    public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.ToTable("products")
                .HasKey(p => new { p.Id });

            builder.HasOne(x => x.Company)
               .WithMany(x => x.Products)
               .HasForeignKey(x => x.CompanyId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.CompanyId)
                .HasColumnName("company_id")
                .HasColumnType("integer");

            builder.Property(p => p.Id)
                .HasColumnName("id")
                .HasColumnType("serial");

            builder.Property(p => p.SkuId)
                .HasColumnName("sku_id")
                .HasColumnType("varchar(20)");

            builder.Property(p => p.Status)
                .HasColumnName("status")
                .HasColumnType("boolean");

            builder.Property(p => p.Barcode)
                .HasColumnName("barcode")
                .HasColumnType("varchar(13)");

            builder.Property(p => p.Description)
                .HasColumnName("description")
                .HasColumnType("varchar(50)");

            builder.Property(p => p.Stock)
                .HasColumnName("stock")
                .HasColumnType("integer");
        }
    }
}
