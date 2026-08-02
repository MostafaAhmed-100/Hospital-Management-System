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
    public interface IUnitOfWork : IDisposable
    {
        ISaleItemRepository SaleItems { get; }
        IPrescriptionItemRepository PrescriptionItems { get; }
        IAppointmentRepository Appointments { get; }
        IClinicRepository Clinics { get; }
        IPharmacySaleRepository PharmacySales { get; }
        IDepartmentRepository Departments { get; }
        IDoctorRepository Doctors { get; }
        IInsuranceProviderRepository InsuranceProviders { get; }
        IInvoiceRepository Invoices { get; }
        IMedicalRecordRepository MedicalRecords { get; }
        IMedicineRepository Medicines { get; }
        IPatientRepository Patients { get; }
        IPaymentRepository Payments { get; }
        IPharmacyInventoryRepository PharmacyInventories { get; }
        IPharmacyRepository Pharmacies { get; }
        IPrescriptionRepository Prescriptions { get; }
        ISpecialtyRepository Specialties { get; }

        Task<int> SaveChangesAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}