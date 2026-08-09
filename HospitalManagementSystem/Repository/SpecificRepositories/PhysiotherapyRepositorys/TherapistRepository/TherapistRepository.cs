using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models.Physiotherapy;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.PhysiotherapyRepositorys.TherapistRepository
{
    public class TherapistRepository : GenericRepository<Therapist>, ITherapistRepository
    {
        public TherapistRepository(Data.AppDbContext appDbcontext) : base(appDbcontext) { }

        public async Task<IEnumerable<Therapist>> GetTherapistsByDepartmentIdAsync(int departmentId)
        {
            return await _AppDbcontext.Set<Therapist>()
                .Include(t => t.Department)
                .Where(t => t.DepartmentId == departmentId)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }
        public async Task<IEnumerable<(string TherapistName, int SessionsCount)>> GetTopActiveTherapistsAsync()
        {
            var topTherapists = await _AppDbcontext.Therapists
                .Where(x => !x.IsDeleted)
                .Select(t => new
                {
                    t.FullName,
                    SessionsCount = t.PhysioSessions.Count() 
                })
                .OrderByDescending(t => t.SessionsCount)
                .Take(5)
                .ToListAsync();

            return topTherapists.Select(x => (x.FullName, x.SessionsCount));
        }
        public async Task<IEnumerable<(string Specialization, int Count)>> GetTherapistSpecializationDistributionAsync()
        {
            var distribution = await _AppDbcontext.Therapists
                .Where(x => !x.IsDeleted && !string.IsNullOrEmpty(x.Specialization))
                .GroupBy(x => x.Specialization)
                .Select(g => new { Specialization = g.Key, Count = g.Count() })
                .ToListAsync();

            return distribution.Select(x => (x.Specialization, x.Count));
        }
    }
}