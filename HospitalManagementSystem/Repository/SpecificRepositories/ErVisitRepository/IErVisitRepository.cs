using HospitalManagementSystem.Data.Models.Emergency;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.ErVisitRepository
{
    public interface IErVisitRepository : IGenericRepository<ErVisit>
    {
        Task<IEnumerable<ErVisit>> GetErQueueAsync();
    }
}
