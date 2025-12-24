using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Domain.Entities;

namespace StoreManagement.Infrastructure.DBContext.EntityFramework.Configurations
{
    public class ProductPriceTypeConfiguration : IEntityTypeConfiguration<ProductPriceEntity>
    {
        public void Configure(EntityTypeBuilder<ProductPriceEntity> builder)
        {
            builder.ToTable("product_price");

            builder.HasKey(pp => pp.Id);
            
            builder.Property(pp => pp.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            
            builder.Property(pp => pp.ProductId)
                .HasColumnName("product_id")
                .IsRequired();
            
            builder.Property(pp => pp.Price)
                .HasColumnName("price")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.HasOne(p => p.Product)
               .WithOne(p => p.ProductPrice)
               .HasForeignKey<ProductPriceEntity>(p => p.ProductId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
