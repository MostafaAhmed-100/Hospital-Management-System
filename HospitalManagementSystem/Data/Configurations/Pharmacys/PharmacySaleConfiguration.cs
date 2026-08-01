using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HospitalManagementSystem.Data.Models.Pharmacys;

namespace HospitalManagementSystem.Data.Configurations.Pharmacys
{
    public class PharmacySaleConfiguration : IEntityTypeConfiguration<PharmacySale>
    {
        public void Configure(EntityTypeBuilder<PharmacySale> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.TotalAmount)
                   .HasColumnType("decimal(18,2)");

            builder.HasOne(p => p.Prescription)
                   .WithOne(pr => pr.PharmacySale)
                   .HasForeignKey<PharmacySale>(p => p.PrescriptionId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}