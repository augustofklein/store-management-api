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
                .HasColumnType("integer");

            builder.Property(i => i.ProductId)
                .HasColumnName("product_id")
                .HasColumnType("integer");

            builder.Property(i => i.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(i => i.Price)
                .HasColumnName("price")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(i => i.Quantity)
                .HasColumnName("quantity")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(i => i.Package)
                .HasColumnName("package")
                .HasColumnType("integer")
                .IsRequired();

            builder.HasOne(c => c.Purchase)
               .WithMany(c => c.PurchaseItems)
               .HasForeignKey(c => c.PurchaseId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Product)
               .WithMany(c => c.PurchaseItems)
               .HasForeignKey(c => c.ProductId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
