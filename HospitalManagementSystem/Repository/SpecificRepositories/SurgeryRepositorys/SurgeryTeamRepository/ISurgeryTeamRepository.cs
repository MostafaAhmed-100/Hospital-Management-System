using HospitalManagementSystem.Data.Models.Surgery;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.SurgeryRepository.SurgeryTeamRepository
{
    public interface ISurgeryTeamRepository : IGenericRepository<SurgeryTeam>
    {
        Task<IEnumerable<SurgeryTeam>> GetTeamBySurgeryIdAsync(int surgeryId);
    }
}
