using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.Surgery;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.SurgeryRepository.SurgeryTeamRepository
{
    public interface ISurgeryTeamRepository : IGenericRepository<SurgeryTeam>
    {
        Task<IEnumerable<SurgeryTeam>> GetTeamBySurgeryIdAsync(int surgeryId);
        Task<IEnumerable<(string StaffName, int Count)>> GetTopActiveSurgeryStaffAsync();
        Task<IEnumerable<(StaffRole Role, int Count)>> GetSurgeryRoleDistributionAsync();
    }
}
