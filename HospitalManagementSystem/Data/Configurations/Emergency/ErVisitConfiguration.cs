using HospitalManagementSystem.Data.Models.Emergency;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Data.Configurations.Emergency
{
    public class ErVisitConfiguration : IEntityTypeConfiguration<ErVisit>
    {
        public void Configure(EntityTypeBuilder<ErVisit> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.ChiefComplaint).IsRequired().HasMaxLength(500);

            builder.HasOne(e => e.Patient).WithMany().HasForeignKey(e => e.PatientId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.AttendingDoctor).WithMany().HasForeignKey(e => e.AttendingDoctorId).OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
