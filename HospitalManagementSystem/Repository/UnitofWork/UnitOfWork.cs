using HospitalManagementSystem.Data;
using HospitalManagementSystem.Repository.SpecificRepositories.ClinicRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.DepartmentRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.DoctorRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.EmergencyRepositorys.ErVisitRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.InpatientRepositorys.AdmissionRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.InpatientRepositorys.BedRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.InpatientRepositorys.RoomRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.InsuranceProviderRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.InvoiceRepository;
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
using HospitalManagementSystem.Repository.SpecificRepositories.SpecialtyRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.SurgeryRepository.OperatingRoomRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.SurgeryRepository.SurgeryRecordRepository;
using HospitalManagementSystem.Repository.SpecificRepositories.SurgeryRepository.SurgeryTeamRepository;
using Microsoft.EntityFrameworkCore.Storage;

namespace HospitalManagementSystem.Repository.UnitofWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IAdmissionRepository Admissions { get; private set; }
        public IBedRepository Beds { get; private set; }
        public IRoomRepository Rooms { get; private set; }
        public IErVisitRepository ErVisits { get; private set; }

        public IOperatingRoomRepository OperatingRooms { get; private set; }
        public ISurgeryRecordRepository SurgeryRecords { get; private set; }
        public ISurgeryTeamRepository SurgeryTeams { get; private set; }
        public IStaffRepository Staff { get; private set; }
        public INurseRepository Nurses { get; private set; }
        public INurseAssignmentRepository NurseAssignments { get; private set; }

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
            Rooms = new RoomRepository(_context);
            Beds = new BedRepository(_context);
            Admissions = new AdmissionRepository(_context);
            ErVisits = new ErVisitRepository(_context);
            OperatingRooms = new OperatingRoomRepository(_context);
            SurgeryRecords = new SurgeryRecordRepository(_context);
            SurgeryTeams = new SurgeryTeamRepository(_context);
            Staff = new StaffRepository(_context);
            Nurses = new NurseRepository(_context);
            NurseAssignments = new NurseAssignmentRepository(_context);
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