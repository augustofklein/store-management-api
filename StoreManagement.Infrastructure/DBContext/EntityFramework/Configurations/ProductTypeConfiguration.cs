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
                .HasColumnType("integer");

            builder.Property(p => p.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

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
                .HasColumnType("varchar(100)");

            builder.Property(p => p.Stock)
                .HasColumnName("stock")
                .HasColumnType("integer");

            builder.Property(pp => pp.AverageCost)
                .HasColumnName("average_cost")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.HasOne(p => p.Company)
               .WithMany(p => p.Products)
               .HasForeignKey(p => p.CompanyId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
