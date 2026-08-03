using HospitalManagementSystem.Data.Models.Surgery;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Data.Configurations.Surgery
{
    public class SurgeryRecordConfiguration : IEntityTypeConfiguration<SurgeryRecord>
    {
        public void Configure(EntityTypeBuilder<SurgeryRecord> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.SurgeryType).IsRequired().HasMaxLength(100);

            builder.HasOne(s => s.Patient).WithMany().HasForeignKey(s => s.PatientId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(s => s.LeadSurgeon).WithMany().HasForeignKey(s => s.LeadSurgeonId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(s => s.OperatingRoom).WithMany(o => o.Surgeries).HasForeignKey(s => s.OperatingRoomId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(s => s.MedicalRecord).WithMany().HasForeignKey(s => s.RecordId).OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(s => !s.IsDeleted);
        }
    }
}
