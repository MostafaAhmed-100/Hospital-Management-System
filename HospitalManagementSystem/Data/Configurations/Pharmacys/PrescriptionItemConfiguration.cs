using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HospitalManagementSystem.Data.Models.Pharmacys;

namespace HospitalManagementSystem.Data.Configurations.Pharmacys
{
    public class PrescriptionItemConfiguration : IEntityTypeConfiguration<PrescriptionItem>
    {
        public void Configure(EntityTypeBuilder<PrescriptionItem> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Prescription)
                   .WithMany(pr => pr.PrescriptionItems) 
                   .HasForeignKey(p => p.PrescriptionId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Medicine)
                   .WithMany(m => m.PrescriptionItems)
                   .HasForeignKey(p => p.MedicineId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}