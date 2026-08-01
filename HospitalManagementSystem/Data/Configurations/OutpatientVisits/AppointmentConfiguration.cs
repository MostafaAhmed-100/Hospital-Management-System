using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HospitalManagementSystem.Data.Models.OutpatientVisits;

namespace HospitalManagementSystem.Data.Configurations.OutpatientVisits
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasOne(a => a.Patient)
                   .WithMany(p => p.Appointments)
                   .HasForeignKey(a => a.PatientId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Doctor)
                   .WithMany(d => d.Appointments)
                   .HasForeignKey(a => a.DoctorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Clinic)
                   .WithMany(c => c.Appointments)
                   .HasForeignKey(a => a.ClinicId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}