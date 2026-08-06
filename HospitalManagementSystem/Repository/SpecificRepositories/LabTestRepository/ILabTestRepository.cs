using HospitalManagementSystem.Data.Models.LabTests;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.LabTestRepository
{
    public interface ILabTestRepository : IGenericRepository<LabTest>
    {
        Task<IEnumerable<LabTest>> GetTestsByRecordIdAsync(int recordId);
        Task<IEnumerable<LabTest>> GetPendingTestsAsync();
    }
}
