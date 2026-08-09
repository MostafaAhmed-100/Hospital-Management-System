using HospitalManagementSystem.Data.Models.Clinics_Doctors;
using HospitalManagementSystem.Data.Models.Emergency;
using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.EmergencyRepositorys.ErVisitRepository
{
    public class ErVisitRepository : GenericRepository<ErVisit>, IErVisitRepository
    {
        public ErVisitRepository(Data.AppDbContext appDbcontext) : base(appDbcontext) { }

        public async Task<IEnumerable<ErVisit>> GetErQueueAsync()
        {
            return await _AppDbcontext.Set<ErVisit>()
                .Include(e => e.Patient)
                .Where(e => e.Status == ErVisitStatus.Pending || e.Status == ErVisitStatus.InTreatment)
                .OrderBy(e => e.TriageLevel)
                .ThenBy(e => e.ArrivalTime)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<IEnumerable<Doctor?>> GetTopDoctorsInErAsync()
        {
            var doctors = await _AppDbcontext.ErVisits
                .Include(x => x.AttendingDoctor)
                .GroupBy(x => x.AttendingDoctor)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .Take(5)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();

            return doctors;
        }

        public async Task<int> GetActiveErVisitsCountAsync()
        {
            var count = await _AppDbcontext.ErVisits
                .Where(x => x.Status != ErVisitStatus.Discharged && !x.IsDeleted)
                .CountAsync();

            return count;
        }

        public async Task<Dictionary<TriageLevel, int>> GetErVisitsDistributionByTriageLevelAsync()
        {
            var distribution = await _AppDbcontext.ErVisits
                .Where(x => !x.IsDeleted)
                .GroupBy(x => x.TriageLevel)
                .Select(g => new { Triage = g.Key, Count = g.Count() })
                .ToDictionaryAsync(k => k.Triage, v => v.Count);

            return distribution;
        }
    }
}
