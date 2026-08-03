using HospitalManagementSystem.Data.Models.Nursing_Staff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Data.Configurations.Nursing_Staff
{
    public class NurseAssignmentConfiguration : IEntityTypeConfiguration<NurseAssignment>
    {
        public void Configure(EntityTypeBuilder<NurseAssignment> builder)
        {
            builder.HasKey(n => n.Id);

            builder.HasOne(n => n.Nurse).WithMany(nu => nu.Assignments).HasForeignKey(n => n.NurseId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(n => n.Admission).WithMany().HasForeignKey(n => n.AdmissionId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(n => n.ErVisit).WithMany().HasForeignKey(n => n.ErVisitId).OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(n => !n.IsDeleted);
        }
    }
}
