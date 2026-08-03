using HospitalManagementSystem.Data.Models.Surgery;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SurgeryTeamConfiguration : IEntityTypeConfiguration<SurgeryTeam>
{
    public void Configure(EntityTypeBuilder<SurgeryTeam> builder)
    {
        builder.HasKey(t => t.Id);

        builder.HasOne(t => t.SurgeryRecord).WithMany(s => s.SurgeryTeams).HasForeignKey(t => t.SurgeryId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(t => t.Staff).WithMany().HasForeignKey(t => t.StaffId).OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(t => !t.IsDeleted);
    }
}