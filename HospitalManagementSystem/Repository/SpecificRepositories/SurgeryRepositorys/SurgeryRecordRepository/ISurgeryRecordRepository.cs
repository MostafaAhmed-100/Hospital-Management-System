using HospitalManagementSystem.Data.Models.Surgery;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.SurgeryRepository.SurgeryRecordRepository
{
    public interface ISurgeryRecordRepository : IGenericRepository<SurgeryRecord>
    {
        Task<bool> HasOverlappingSurgeryAsync(int orId, DateTime startTime, DateTime endTime, int? excludeSurgeryId = null);
    }
}
