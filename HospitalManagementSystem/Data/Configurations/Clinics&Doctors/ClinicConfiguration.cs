using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HospitalManagementSystem.Data.Models.Clinics_Doctors;

namespace HospitalManagementSystem.Data.Configurations.Clinics_Doctors
{
    public class ClinicConfiguration : IEntityTypeConfiguration<Clinic>
    {
        public void Configure(EntityTypeBuilder<Clinic> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasOne(c => c.Department)
                   .WithMany(d => d.Clinics)
                   .HasForeignKey(c => c.DepartmentId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}