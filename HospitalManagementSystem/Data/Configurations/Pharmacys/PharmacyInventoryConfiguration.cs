using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HospitalManagementSystem.Data.Models.Pharmacys;

namespace HospitalManagementSystem.Data.Configurations.Pharmacys
{
    public class PharmacyInventoryConfiguration : IEntityTypeConfiguration<PharmacyInventory>
    {
        public void Configure(EntityTypeBuilder<PharmacyInventory> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Medicine)
                   .WithMany(m => m.PharmacyInventories)
                   .HasForeignKey(p => p.MedicineId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}