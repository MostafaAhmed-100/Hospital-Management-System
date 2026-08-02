using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HospitalManagementSystem.Data.Models.Clinics_Doctors;

namespace HospitalManagementSystem.Data.Configurations.Clinics_Doctors
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(d => d.Id);

            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasMany(d => d.Clinics)
                   .WithOne(c => c.Department)
                   .HasForeignKey(c => c.DepartmentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(d => d.Doctors)
                   .WithOne(doc => doc.Department)
                   .HasForeignKey(doc => doc.DepartmentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}