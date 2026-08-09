using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.Surgery;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.SurgeryRepository.SurgeryRecordRepository
{
    public interface ISurgeryRecordRepository : IGenericRepository<SurgeryRecord>
    {
        Task<IEnumerable<(SurgeryStatus Status, int Count)>> GetSurgeryStatusDistributionAsync();
        Task<IEnumerable<(string SurgeryType, int Count)>> GetTopSurgeryTypesAsync();
        Task<bool> HasOverlappingSurgeryAsync(int orId, DateTime startTime, DateTime endTime, int? excludeSurgeryId = null);
    }
}
