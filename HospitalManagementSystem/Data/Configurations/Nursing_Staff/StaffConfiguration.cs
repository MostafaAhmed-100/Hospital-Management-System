using HospitalManagementSystem.Data.Models.Nursing_Staff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Data.Configurations.Nursing_Staff
{
    public class StaffConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.FullName).IsRequired().HasMaxLength(150);

            builder.HasOne(s => s.Clinic).WithMany().HasForeignKey(s => s.ClinicId).OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(s => !s.IsDeleted);
        }
    }
}
