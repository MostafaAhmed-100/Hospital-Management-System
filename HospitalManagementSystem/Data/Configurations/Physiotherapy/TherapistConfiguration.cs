using HospitalManagementSystem.Data.Models.Physiotherapy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Data.Configurations.Physiotherapy
{
    public class TherapistConfiguration : IEntityTypeConfiguration<Therapist>
    {
        public void Configure(EntityTypeBuilder<Therapist> builder)
        {
            builder.ToTable("Therapists");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.FullName).IsRequired().HasMaxLength(150);
            builder.Property(t => t.Specialization).IsRequired().HasMaxLength(100);

            builder.HasQueryFilter(t => !t.IsDeleted);

            builder.HasOne(t => t.Department)
                   .WithMany()
                   .HasForeignKey(t => t.DepartmentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
