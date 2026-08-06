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
            return await _AppDbcontext.Set<PhysioSession>()
                .Include(s => s.Therapist)
                .Where(s => s.PatientId == patientId)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }

        public async Task<IEnumerable<PhysioSession>> GetSessionsByTherapistIdAsync(int therapistId)
        {
            return await _AppDbcontext.Set<PhysioSession>()
                .Include(s => s.Patient)
                .Where(s => s.TherapistId == therapistId)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }
    }
}
