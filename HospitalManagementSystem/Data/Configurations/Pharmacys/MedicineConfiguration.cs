using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HospitalManagementSystem.Data.Models.Pharmacys;

namespace HospitalManagementSystem.Data.Configurations.Pharmacys
{
    public class MedicineConfiguration : IEntityTypeConfiguration<Medicine>
    {
        public void Configure(EntityTypeBuilder<Medicine> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.UnitPrice)
                   .HasColumnType("decimal(18,2)");
        }
    }
}