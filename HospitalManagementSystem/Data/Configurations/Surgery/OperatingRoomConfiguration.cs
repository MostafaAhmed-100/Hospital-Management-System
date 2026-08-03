using HospitalManagementSystem.Data.Models.Surgery;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.Data.Configurations.Surgery
{
    public class OperatingRoomConfiguration : IEntityTypeConfiguration<OperatingRoom>
    {
        public void Configure(EntityTypeBuilder<OperatingRoom> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.RoomNumber).IsRequired().HasMaxLength(50);
            builder.HasQueryFilter(o => !o.IsDeleted);
        }
    }
}
