using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Domain.Entities;

namespace StoreManagement.Infrastructure.DBContext.EntityFramework.Configurations
{
    public class UserCompanyTyperConfiguration : IEntityTypeConfiguration<UserCompanyEntity>
    {
        public void Configure(EntityTypeBuilder<UserCompanyEntity> builder)
        {
            builder.ToTable("user_companies");
            
            builder.HasKey(uc => uc.Id);
            
            builder.Property(uc => uc.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(uc => uc.CompanyId)
                .HasColumnName("company_id")
                .HasColumnType("int");
            
            builder.Property(uc => uc.UserId)
                .HasColumnName("user_id")
                .HasColumnType("int");
            
            builder.Property(uc => uc.RoleId)
                .HasColumnName("role_id")
                .HasColumnType("int");

            builder.Property(u => u.IsActive)
                .HasColumnName("is_active")
                .HasColumnType("boolean");

            builder.Property(u => u.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamp");

            // TODO: Review the DeleteBehavior property
            builder.HasOne(uc => uc.Company)
               .WithMany(c => c.UserCompanies)
               .HasForeignKey(uc => uc.CompanyId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(uc => uc.User)
                .WithMany(u => u.UserCompanies)
                .HasForeignKey(uc => uc.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(uc => uc.Role)
                .WithMany()
                .HasForeignKey(uc => uc.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
