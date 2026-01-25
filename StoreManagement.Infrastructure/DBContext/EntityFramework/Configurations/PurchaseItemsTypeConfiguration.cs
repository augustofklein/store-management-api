using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Domain.Entities;

namespace StoreManagement.Infrastructure.DBContext.EntityFramework.Configurations
{
    public class PurchaseItemsTypeConfiguration : IEntityTypeConfiguration<PurchaseItemEntity>
    {
        public void Configure(EntityTypeBuilder<PurchaseItemEntity> builder)
        {
            builder.ToTable("purchase_items");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.PurchaseId)
                .HasColumnName("purchase_id")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(i => i.ProductId)
                .HasColumnName("product_id")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(i => i.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(i => i.Price)
                .HasColumnName("price")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(i => i.Package)
                .HasColumnName("package")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(i => i.Quantity)
                .HasColumnName("quantity")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(i => i.ShippingCost)
                .HasColumnName("shipping_cost")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.HasOne(c => c.Purchase)
               .WithMany(c => c.PurchaseItems)
               .HasForeignKey(c => c.PurchaseId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Product)
               .WithMany(c => c.PurchaseItems)
               .HasForeignKey(c => c.ProductId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(pi => pi.PurchaseId)
                .HasDatabaseName("idx_purchase_items_purchase_id");
            
            builder.HasIndex(pi => pi.ProductId)
                .HasDatabaseName("idx_purchase_items_product_id");
            
            builder.HasIndex(pi => new { pi.PurchaseId, pi.ProductId })
                .IsUnique();
        }
    }
}
