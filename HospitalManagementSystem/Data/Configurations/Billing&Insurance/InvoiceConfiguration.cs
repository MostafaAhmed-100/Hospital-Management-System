using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HospitalManagementSystem.Data.Models.Billing_Insurance;

namespace HospitalManagementSystem.Data.Configurations.Billing_Insurance
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Amount)
                   .HasColumnType("decimal(18,2)");

            builder.HasOne(i => i.Appointment)
                   .WithOne(a => a.Invoice)
                   .HasForeignKey<Invoice>(i => i.AppointmentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Patient)
                   .WithMany(p => p.Invoices)
                   .HasForeignKey(i => i.PatientId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}