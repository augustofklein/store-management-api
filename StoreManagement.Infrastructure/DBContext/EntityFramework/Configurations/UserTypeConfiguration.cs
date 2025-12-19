using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Infrastructure.DBContext.Model;

namespace StoreManagement.Infrastructure.DBContext.EntityFramework.Configurations
{
    public class UserTypeConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(u => u.CompanyId)
                .HasColumnName("company_id")
                .HasColumnType("int");

            builder.Property(u => u.Id)
                .HasColumnName("id")
                .HasColumnType("serial");

            builder.Property(u => u.Email)
                .HasColumnName("email")
                .HasColumnType("varchar(50)");

            builder.Property(u => u.PasswordHash)
                .HasColumnName("password_hash")
                .HasColumnType("varchar(200)");

            builder.HasOne(u => u.Company)
               .WithMany(u => u.Users)
               .HasForeignKey(u => u.CompanyId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
