using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models.Surgery;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.SurgeryRepository.SurgeryTeamRepository
{
    public class SurgeryTeamRepository : GenericRepository<SurgeryTeam>, ISurgeryTeamRepository
    {
        public SurgeryTeamRepository(AppDbContext appDbcontext) : base(appDbcontext) { }

        public async Task<IEnumerable<SurgeryTeam>> GetTeamBySurgeryIdAsync(int surgeryId)
        {
            return await _AppDbcontext.Set<SurgeryTeam>()
                .Include(t => t.Staff)
                .Where(t => t.SurgeryId == surgeryId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
