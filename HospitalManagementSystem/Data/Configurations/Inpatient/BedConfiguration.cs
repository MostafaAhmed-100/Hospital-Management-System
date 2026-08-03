using HospitalManagementSystem.Data.Models.Inpatient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Data.Configurations.Inpatient
{
    public class BedConfiguration : IEntityTypeConfiguration<Bed>
    {
        public void Configure(EntityTypeBuilder<Bed> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.BedNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(b => b.Status)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(b => b.Room)
                .WithMany(r => r.Beds)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(b => !b.IsDeleted);
        }
    }
}
