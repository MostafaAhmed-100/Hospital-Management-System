using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HospitalManagementSystem.Data.Models.Billing_Insurance;

namespace HospitalManagementSystem.Data.Configurations.Billing_Insurance
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Amount)
                   .HasColumnType("decimal(18,2)");

            builder.HasOne(p => p.Invoice)
                   .WithMany(i => i.Payments)
                   .HasForeignKey(p => p.InvoiceId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}