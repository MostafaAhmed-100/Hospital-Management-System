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
    }
}
