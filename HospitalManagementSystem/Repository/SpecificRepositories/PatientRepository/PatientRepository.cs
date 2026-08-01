using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models.OutpatientVisits;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.PatientRepository
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
    }
}
