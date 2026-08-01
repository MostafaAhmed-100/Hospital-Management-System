using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HospitalManagementSystem.Data.Models.Pharmacys;

namespace HospitalManagementSystem.Data.Configurations.Pharmacys
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.UnitPrice)
                   .HasColumnType("decimal(18,2)");
        }
    }
}