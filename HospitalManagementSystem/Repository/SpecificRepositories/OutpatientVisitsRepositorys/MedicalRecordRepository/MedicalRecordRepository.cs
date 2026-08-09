using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models.OutpatientVisits;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.OutpatientVisitsRepository.MedicalRecordRepository
{
    public class MedicalRecordRepository : GenericRepository<MedicalRecord>, IMedicalRecordRepository
    {
        public MedicalRecordRepository(AppDbContext appDbcontext) : base(appDbcontext)
        {
        }

        public async Task<IEnumerable<MedicalRecord?>> GetRecordsByPatientIdAsync(int patientId)
        {
            var Records = await _AppDbcontext.MedicalRecords
                .Include(x => x.Doctor)
                .Include(x => x.Appointment) 
                .Where(x => x.PatientId == patientId)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();

            return Records;
        }
        public async Task<IEnumerable<(string Diagnosis, int Count)>> GetTopDiagnosesAsync()
        {
            var topDiagnoses = await _AppDbcontext.MedicalRecords
                .Where(x => !x.IsDeleted && !string.IsNullOrEmpty(x.Diagnosis))
                .GroupBy(x => x.Diagnosis)
                .Select(g => new { Diagnosis = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToListAsync();

            return topDiagnoses.Select(x => (x.Diagnosis, x.Count));
        }
        public async Task<int> GetTodayMedicalRecordsCountAsync()
        {
            var today = DateTime.Today;

            var count = await _AppDbcontext.MedicalRecords
                .Where(x => !x.IsDeleted && x.CreatedAt.Date == today)
                .CountAsync();

            return count;
        }
    }
}