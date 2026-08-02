using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HospitalManagementSystem.Data.Models.Billing_Insurance;

namespace HospitalManagementSystem.Data.Configurations.Billing_Insurance
{
    public class InsuranceProviderConfiguration : IEntityTypeConfiguration<InsuranceProvider>
    {
        public void Configure(EntityTypeBuilder<InsuranceProvider> builder)
        {
            builder.HasKey(i => i.Id);

            builder.HasMany(i => i.Patients)
                   .WithOne(p => p.InsuranceProvider)
                   .HasForeignKey(p => p.InsuranceId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.SetNull);
            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}