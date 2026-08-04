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
    }
}
