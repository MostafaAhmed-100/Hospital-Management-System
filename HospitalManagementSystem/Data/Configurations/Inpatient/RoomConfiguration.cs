using HospitalManagementSystem.Data.Models.Inpatient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Data.Configurations.Inpatient
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.RoomNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(r => r.RoomType)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(r => r.Department)
                .WithMany() 
                .HasForeignKey(r => r.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasQueryFilter(r => !r.IsDeleted);
        }
    }
}
