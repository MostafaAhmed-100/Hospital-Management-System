using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models.Enums;
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
        public async Task<IEnumerable<(string StaffName, int Count)>> GetTopActiveSurgeryStaffAsync()
        {
            var topStaff = await _AppDbcontext.SurgeryTeams
                .Include(x => x.Staff)
                .GroupBy(x => x.Staff.FullName)
                .Select(g => new { StaffName = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToListAsync();

            return topStaff.Select(x => (x.StaffName, x.Count));
        }
        public async Task<IEnumerable<(StaffRole Role, int Count)>> GetSurgeryRoleDistributionAsync()
        {
            var distribution = await _AppDbcontext.SurgeryTeams
                .Where(x => !x.IsDeleted)
                .GroupBy(x => x.RoleInSurgery)
                .Select(g => new { Role = g.Key, Count = g.Count() })
                .ToListAsync();

            return distribution.Select(x => (x.Role, x.Count));
        }
    }
}
