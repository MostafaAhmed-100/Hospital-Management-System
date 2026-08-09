using HospitalManagementSystem.Data.Models.Clinics_Doctors;
using HospitalManagementSystem.Data.Models.Emergency;
using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.EmergencyRepositorys.ErVisitRepository
{
    public interface IErVisitRepository : IGenericRepository<ErVisit>
    {
        Task<IEnumerable<ErVisit>> GetErQueueAsync();
        Task<IEnumerable<Doctor?>> GetTopDoctorsInErAsync();
        Task<int> GetActiveErVisitsCountAsync();
        Task<Dictionary<TriageLevel, int>> GetErVisitsDistributionByTriageLevelAsync();
    }
}
