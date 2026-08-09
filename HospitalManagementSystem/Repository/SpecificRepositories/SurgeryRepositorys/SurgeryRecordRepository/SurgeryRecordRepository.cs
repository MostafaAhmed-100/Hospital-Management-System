using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.Surgery;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.SurgeryRepository.SurgeryRecordRepository
{
    public class SurgeryRecordRepository : GenericRepository<SurgeryRecord>, ISurgeryRecordRepository
    {
        public SurgeryRecordRepository(Data.AppDbContext appDbcontext) : base(appDbcontext) { }

        public async Task<bool> HasOverlappingSurgeryAsync(int orId, DateTime startTime, DateTime endTime, int? excludeSurgeryId = null)
        {
            var query = _AppDbcontext.Set<SurgeryRecord>()
                .Where(s => s.OperatingRoomId == orId && s.Status != SurgeryStatus.Cancelled);

            if (excludeSurgeryId.HasValue)
            {
                query = query.Where(s => s.Id != excludeSurgeryId.Value);
            }

            return await query.AnyAsync(s =>
                (startTime >= s.StartTime && startTime < s.EndTime) ||
                (endTime > s.StartTime && endTime <= s.EndTime) ||
                (startTime <= s.StartTime && endTime >= s.EndTime));
        }
        public async Task<IEnumerable<(string SurgeryType, int Count)>> GetTopSurgeryTypesAsync()
        {
            var topSurgeries = await _AppDbcontext.SurgeryRecords
                .GroupBy(x => x.SurgeryType)
                .Select(g => new { SurgeryType = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToListAsync();

            return topSurgeries.Select(x => (x.SurgeryType, x.Count));
        }
        public async Task<IEnumerable<(SurgeryStatus Status, int Count)>> GetSurgeryStatusDistributionAsync()
        {
            var distribution = await _AppDbcontext.SurgeryRecords
                .GroupBy(x => x.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToListAsync();

            return distribution.Select(x => (x.Status, x.Count));
        }
    }
}
