using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models.OutpatientVisits;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.MedicalRecordRepository
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
    }
}