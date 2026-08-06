using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Data.Models.Billing_Insurance;
using HospitalManagementSystem.Data.Models.Clinics_Doctors;
using HospitalManagementSystem.Data.Models.Emergency;
using HospitalManagementSystem.Data.Models.Inpatient;
using HospitalManagementSystem.Data.Models.LabTests;
using HospitalManagementSystem.Data.Models.Nursing_Staff;
using HospitalManagementSystem.Data.Models.OutpatientVisits;
using HospitalManagementSystem.Data.Models.Pharmacys;
using HospitalManagementSystem.Data.Models.Physiotherapy;
using HospitalManagementSystem.Data.Models.Surgery;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Therapist> Therapists { get; set; }
        public DbSet<PhysioSession> PhysioSessions { get; set; }
        public DbSet<LabTest> LabTests  { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<PharmacyInventory> PharmacyInventories { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescriptionItem> PrescriptionItems { get; set; }
        public DbSet<PharmacySale> PharmacySales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }
        public DbSet<InsuranceProvider> InsuranceProviders { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Bed> Beds { get; set; }
        public DbSet<Admission> Admissions { get; set; }
        public DbSet<ErVisit> ErVisits { get; set; }
        public DbSet<OperatingRoom> OperatingRooms { get; set; }
        public DbSet<SurgeryRecord> SurgeryRecords { get; set; }
        public DbSet<SurgeryTeam> SurgeryTeams { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Nurse> Nurses { get; set; }
        public DbSet<NurseAssignment> NurseAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}