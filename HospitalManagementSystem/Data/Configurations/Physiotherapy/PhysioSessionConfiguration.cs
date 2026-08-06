using HospitalManagementSystem.Data.Models.Physiotherapy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Data.Configurations.Physiotherapy
{
    public class PhysioSessionConfiguration : IEntityTypeConfiguration<PhysioSession>
    {
        public void Configure(EntityTypeBuilder<PhysioSession> builder)
        {
            builder.ToTable("PhysioSessions");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.TherapyType).IsRequired().HasMaxLength(100);
            builder.Property(p => p.ProgressNotes).HasMaxLength(1000);

            builder.HasQueryFilter(p => !p.IsDeleted);

            builder.HasOne(p => p.Patient)
                   .WithMany()
                   .HasForeignKey(p => p.PatientId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Therapist)
                   .WithMany(t => t.PhysioSessions)
                   .HasForeignKey(p => p.TherapistId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.MedicalRecord)
                   .WithMany()
                   .HasForeignKey(p => p.RecordId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
