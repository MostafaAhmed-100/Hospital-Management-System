using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HospitalManagementSystem.Data.Models.Pharmacys;

namespace HospitalManagementSystem.Data.Configurations.Pharmacys
{
    public class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Record) 
                   .WithMany(m => m.Prescriptions)
                   .HasForeignKey(p => p.RecordId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Doctor) 
                   .WithMany(d => d.Prescriptions)
                   .HasForeignKey(p => p.DoctorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Patient)
                   .WithMany(pat => pat.Prescriptions)
                   .HasForeignKey(p => p.PatientId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}