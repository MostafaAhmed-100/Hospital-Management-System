using HospitalManagementSystem.Data.Models.LabTests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Data.Configurations.LabTests
{
    public class LabTestConfiguration : IEntityTypeConfiguration<LabTest>
    {
        public void Configure(EntityTypeBuilder<LabTest> builder)
        {
            builder.ToTable("LabTests");
            builder.HasKey(l => l.Id);

            builder.Property(l => l.TestName).IsRequired().HasMaxLength(150);
            builder.Property(l => l.Result).HasMaxLength(1000);

            builder.HasQueryFilter(l => !l.IsDeleted);

            builder.HasOne(l => l.MedicalRecord)
                   .WithMany()
                   .HasForeignKey(l => l.RecordId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
