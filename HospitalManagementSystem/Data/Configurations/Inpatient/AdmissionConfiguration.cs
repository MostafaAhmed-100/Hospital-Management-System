using HospitalManagementSystem.Data.Models.Inpatient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Data.Configurations.Inpatient
{
    public class AdmissionConfiguration : IEntityTypeConfiguration<Admission>
    {
        public void Configure(EntityTypeBuilder<Admission> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Reason)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(a => a.Status)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(a => a.Patient)
                .WithMany()
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Doctor)
                .WithMany()
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Bed)
                .WithMany(b => b.Admissions)
                .HasForeignKey(a => a.BedId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.MedicalRecord)
                .WithMany()
                .HasForeignKey(a => a.RecordId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(a => !a.IsDeleted);
        }
    }
}
