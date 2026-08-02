using HospitalManagementSystem.Data;
using HospitalManagementSystem.Repository.SpecificRepositories.AppointmentRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.ClinicRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.DepartmentRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.DoctorRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.InsuranceProviderRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.InvoiceRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.MedicalRecordRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.MedicineRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.PatientRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.PaymentRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.PharmacyInventoryRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.PharmacyRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.PharmacySaleRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.PrescriptionItemRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.PrescriptionRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.SaleItemRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.SpecialtyRepository;
using Microsoft.EntityFrameworkCore.Storage;

namespace HospitalManagementSystem.Repository.UnitofWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        
        public ISaleItemRepository SaleItems { get; private set; }
        public IPrescriptionItemRepository PrescriptionItems { get; private set; }
        public IAppointmentRepository Appointments { get; private set; }
        public IPharmacySaleRepository PharmacySales { get; private set; }
        public IClinicRepository Clinics { get; private set; }
        public IDepartmentRepository Departments { get; private set; }
        public IDoctorRepository Doctors { get; private set; }
        public IInsuranceProviderRepository InsuranceProviders { get; private set; }
        public IInvoiceRepository Invoices { get; private set; }
        public IMedicalRecordRepository MedicalRecords { get; private set; }
        public IMedicineRepository Medicines { get; private set; }
        public IPatientRepository Patients { get; private set; }
        public IPaymentRepository Payments { get; private set; }
        public IPharmacyInventoryRepository PharmacyInventories { get; private set; }
        public IPharmacyRepository Pharmacies { get; private set; }
        public IPrescriptionRepository Prescriptions { get; private set; }
        public ISpecialtyRepository Specialties { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            Appointments = new AppointmentRepository(_context);
            Clinics = new ClinicRepository(_context);
            Departments = new DepartmentRepository(_context);
            Doctors = new DoctorRepository(_context);
            InsuranceProviders = new InsuranceProviderRepository(_context);
            Invoices = new InvoiceRepository(_context);
            MedicalRecords = new MedicalRecordRepository(_context);
            Medicines = new MedicineRepository(_context);
            Patients = new PatientRepository(_context);
            Payments = new PaymentRepository(_context);
            PharmacyInventories = new PharmacyInventoryRepository(_context);
            Pharmacies = new PharmacyRepository(_context);
            Prescriptions = new PrescriptionRepository(_context);
            Specialties = new SpecialtyRepository(_context);
            PharmacySales = new PharmacySaleRepository(_context);
            PrescriptionItems = new PrescriptionItemRepository(_context);
            SaleItems = new SaleItemRepository(_context);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}