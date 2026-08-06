using HospitalManagementSystem.Data.Models.Physiotherapy;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.PhysiotherapyRepositorys.PhysioSessionRepository
{
    public interface IPhysioSessionRepository : IGenericRepository<PhysioSession>
    {
        Task<IEnumerable<PhysioSession>> GetSessionsByPatientIdAsync(int patientId);
        Task<IEnumerable<PhysioSession>> GetSessionsByTherapistIdAsync(int therapistId);
    }
}
