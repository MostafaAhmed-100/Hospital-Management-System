using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HospitalManagementSystem.Data.Models.Pharmacys;

namespace HospitalManagementSystem.Data.Configurations.Pharmacys
{
    public class PharmacyConfiguration : IEntityTypeConfiguration<Pharmacy>
    {
        public void Configure(EntityTypeBuilder<Pharmacy> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasMany(p => p.PharmacySales)
                   .WithOne(s => s.Pharmacy)
                   .HasForeignKey(s => s.PharmacyId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.PharmacyInventories)
                   .WithOne(i => i.Pharmacy)
                   .HasForeignKey(i => i.PharmacyId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}