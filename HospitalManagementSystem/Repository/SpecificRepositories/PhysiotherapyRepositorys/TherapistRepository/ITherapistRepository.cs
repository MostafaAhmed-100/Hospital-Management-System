using HospitalManagementSystem.Data.Models.Physiotherapy;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.PhysiotherapyRepositorys.TherapistRepository
{
    public interface ITherapistRepository : IGenericRepository<Therapist>
    {
        Task<IEnumerable<Therapist>> GetTherapistsByDepartmentIdAsync(int departmentId);
    }
}
