using HospitalManagementSystem.Repository.SpecificRepositories.ClinicRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.DepartmentRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.DoctorRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.EmergencyRepositorys.ErVisitRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.InpatientRepositorys.AdmissionRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.InpatientRepositorys.BedRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.InpatientRepositorys.RoomRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.InsuranceProviderRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.InvoiceRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.LabTestRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.Nursing_StaffRepositorys.NurseAssignmentRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.Nursing_StaffRepositorys.NurseRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.Nursing_StaffRepositorys.StaffRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.OutpatientVisitsRepository.AppointmentRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.OutpatientVisitsRepository.MedicalRecordRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.OutpatientVisitsRepository.PatientRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.PaymentRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.PharmacysRepository.MedicineRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.PharmacysRepository.PharmacyInventoryRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.PharmacysRepository.PharmacyRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.PharmacysRepository.PharmacySaleRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.PharmacysRepository.PrescriptionItemRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.PharmacysRepository.PrescriptionRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.PharmacysRepository.SaleItemRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.PhysiotherapyRepositorys.PhysioSessionRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.PhysiotherapyRepositorys.TherapistRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.SpecialtyRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.SurgeryRepository.OperatingRoomRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.SurgeryRepository.SurgeryRecordRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.SurgeryRepository.SurgeryTeamRepository;
using Microsoft.EntityFrameworkCore.Storage;

namespace HospitalManagementSystem.Repository.UnitofWork
{
    public interface IUnitOfWork : IDisposable
    {
        IAdmissionRepository Admissions { get; }
        IBedRepository Beds { get; }
        IRoomRepository Rooms { get; }
        IErVisitRepository ErVisits { get; }

        IOperatingRoomRepository OperatingRooms { get; }
        ISurgeryRecordRepository SurgeryRecords { get; }
        ISurgeryTeamRepository SurgeryTeams { get; }
        IStaffRepository Staff { get; }
        INurseRepository Nurses { get; }
        INurseAssignmentRepository NurseAssignments { get; }
        ILabTestRepository LabTests { get; }
        ITherapistRepository Therapists { get; }
        IPhysioSessionRepository PhysioSessions { get; }
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