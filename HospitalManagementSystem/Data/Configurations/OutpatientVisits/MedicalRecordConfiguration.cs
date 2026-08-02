using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HospitalManagementSystem.Data.Models.OutpatientVisits;

namespace HospitalManagementSystem.Data.Configurations.OutpatientVisits
{
    public class MedicalRecordConfiguration : IEntityTypeConfiguration<MedicalRecord>
    {
        public void Configure(EntityTypeBuilder<MedicalRecord> builder)
        {
            builder.HasKey(m => m.Id);

            builder.HasOne(m => m.Appointment)
                   .WithOne(a => a.MedicalRecord)
                   .HasForeignKey<MedicalRecord>(m => m.AppointmentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(m => m.Doctor)
                   .WithMany(d => d.MedicalRecords)
                   .HasForeignKey(m => m.DoctorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasOne(m => m.Patient)
                   .WithMany(p => p.MedicalRecords)
                   .HasForeignKey(m => m.PatientId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}