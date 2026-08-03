using HospitalManagementSystem.Data.Models.Nursing_Staff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Data.Configurations.Nursing_Staff
{
    public class NurseConfiguration : IEntityTypeConfiguration<Nurse>
    {
        public void Configure(EntityTypeBuilder<Nurse> builder)
        {
            builder.HasKey(n => n.Id);
            builder.Property(n => n.LicenseNumber).IsRequired().HasMaxLength(50);

            builder.HasOne(n => n.Staff).WithOne(s => s.NurseDetails).HasForeignKey<Nurse>(n => n.StaffId).OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(n => !n.IsDeleted);
        }
    }
}
