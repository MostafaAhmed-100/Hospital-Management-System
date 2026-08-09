using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models.OutpatientVisits;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.OutpatientVisitsRepository.PatientRepository
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        public PatientRepository(AppDbContext appDbcontext) : base(appDbcontext)
        {
        }

        public async Task<Patient?> GetPatientWithMedicalHistoryAsync(int patientId)
        {
            var Patient = await _AppDbcontext.Patients
                .Include(x=>x.MedicalRecords)
                .ThenInclude(x => x.Appointment)
                .AsSplitQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == patientId);
            return Patient;
        }
        public async Task<IEnumerable<(string Category, int Count)>> GetPatientInsuranceDistributionAsync()
        {
            var insuredCount = await _AppDbcontext.Patients
                .Where(x => !x.IsDeleted && x.InsuranceId != null)
                .CountAsync();

            var nonInsuredCount = await _AppDbcontext.Patients
                .Where(x => !x.IsDeleted && x.InsuranceId == null)
                .CountAsync();

            var result = new List<(string Category, int Count)>
            {
                ("Insured", insuredCount),
                ("Non-Insured", nonInsuredCount)
            };

            return result;
        }
        public async Task<IEnumerable<(string PatientName, int AppointmentsCount)>> GetTopFrequentPatientsAsync()
        {
            var topPatients = await _AppDbcontext.Patients
                .Where(x => !x.IsDeleted)
                .Select(p => new
                {
                    p.FullName,
                    AppointmentsCount = p.Appointments.Count(a => !a.IsDeleted)
                })
                .OrderByDescending(p => p.AppointmentsCount)
                .Take(5)
                .ToListAsync();

            return topPatients.Select(x => (x.FullName, x.AppointmentsCount));
        }
    }
}