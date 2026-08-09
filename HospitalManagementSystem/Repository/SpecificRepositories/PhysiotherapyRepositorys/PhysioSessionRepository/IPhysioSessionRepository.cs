using HospitalManagementSystem.Data.Models.Physiotherapy;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.PhysiotherapyRepositorys.PhysioSessionRepository
{
    public interface IPhysioSessionRepository : IGenericRepository<PhysioSession>
    {
        Task<IEnumerable<PhysioSession>> GetSessionsByPatientIdAsync(int patientId);
        Task<IEnumerable<PhysioSession>> GetSessionsByTherapistIdAsync(int therapistId);
        Task<IEnumerable<PhysioSession>> GetSessionsByRecordIdAsync(int recordId);
        Task<IEnumerable<(string TherapyType, int Count)>> GetTopTherapyTypesAsync();
        Task<int> GetTodayPhysioSessionsCountAsync();
    }
}
