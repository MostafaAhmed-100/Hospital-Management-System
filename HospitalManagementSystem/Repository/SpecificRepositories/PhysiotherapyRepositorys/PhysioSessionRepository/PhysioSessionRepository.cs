using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models.Physiotherapy;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.PhysiotherapyRepositorys.PhysioSessionRepository
{
    public class PhysioSessionRepository : GenericRepository<PhysioSession>, IPhysioSessionRepository
    {
        public PhysioSessionRepository(Data.AppDbContext appDbcontext) : base(appDbcontext) { }

        public async Task<IEnumerable<PhysioSession>> GetSessionsByPatientIdAsync(int patientId)
        {
            return await _AppDbcontext.PhysioSessions
                .Include(s => s.Therapist)
                .Where(s => s.PatientId == patientId)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }

        public async Task<IEnumerable<PhysioSession>> GetSessionsByTherapistIdAsync(int therapistId)
        {
            return await _AppDbcontext.PhysioSessions
                .Include(s => s.Patient)
                .Where(s => s.TherapistId == therapistId)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }
        public async Task<IEnumerable<PhysioSession>> GetSessionsByRecordIdAsync(int recordId)
        {
            return await _AppDbcontext.PhysioSessions
                .Include(s => s.Patient)
                .Include(s => s.Therapist)
                .Where(s => s.RecordId == recordId)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }
    }
}
