using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Infrastructure.DBContext.Model;

namespace StoreManagement.Infrastructure.DBContext.EntityFramework.Configurations
{
    public class CompanyTypeConfiguration : IEntityTypeConfiguration<CompanyEntity>
    {
        public void Configure(EntityTypeBuilder<CompanyEntity> builder)
        {
            builder.ToTable("companies")
                .HasKey(company => new { company.Id });

            builder.Property(company => company.Id)
                .HasColumnName("id")
                .HasColumnType("serial");

            builder.Property(company => company.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(50)");
        }
    }
}
