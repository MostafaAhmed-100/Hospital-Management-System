using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.LabTests;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.LabTestRepository
{
    public interface ILabTestRepository : IGenericRepository<LabTest>
    {
        Task<IEnumerable<LabTest>> GetTestsByRecordIdAsync(int recordId);
        Task<IEnumerable<LabTest>> GetPendingTestsAsync();
        Task<IEnumerable<(LabTestStatus Status, int Count)>> GetLabTestStatusDistributionAsync();
        Task<IEnumerable<(string TestName, int Count)>> GetTopRequestedLabTestsAsync();
    }
}
