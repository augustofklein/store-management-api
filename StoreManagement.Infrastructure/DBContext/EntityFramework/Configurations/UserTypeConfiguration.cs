using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Infrastructure.DBContext.Model;

namespace StoreManagement.Infrastructure.DBContext.EntityFramework.Configurations
{
    public class UserTypeConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("users")
                .HasKey(user => new { user.Id });

            builder.Property(user => user.CompanyId)
                .HasColumnName("company_id")
                .HasColumnType("int");

            builder.Property(user => user.Id)
                .HasColumnName("id")
                .HasColumnType("serial");

            builder.Property(user => user.Email)
                .HasColumnName("email")
                .HasColumnType("varchar(50)");

            builder.Property(user => user.PasswordHash)
                .HasColumnName("password_hash")
                .HasColumnType("varchar(200)");
        }
    }
}
