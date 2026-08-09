using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.Inpatient;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.InpatientRepositorys.BedRepository
{
    public class BedRepository : GenericRepository<Bed>, IBedRepository
    {
        public BedRepository(AppDbContext appDbcontext) : base(appDbcontext) { }

        public async Task<IEnumerable<Bed>> GetAvailableBedsAsync()
        {
            return await _AppDbcontext.Beds
                .Include(b => b.Room)
                .Where(b => b.Status == BedStatus.Available)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<int> GetAvailableBedsCountAsync()
        {
            var count = await _AppDbcontext.Beds
                .Where(x => x.Status == BedStatus.Available && !x.IsDeleted)
                .CountAsync();

            return count;
        }

        public async Task<Dictionary<BedStatus, int>> GetBedsDistributionByStatusAsync()
        {
            var distribution = await _AppDbcontext.Beds
                .Where(x => !x.IsDeleted)
                .GroupBy(x => x.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToDictionaryAsync(k => k.Status, v => v.Count);

            return distribution;
        }
    }
}