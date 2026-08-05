using HospitalManagementSystem.Data.Models.Nursing_Staff;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.Nursing_StaffRepositorys.NurseAssignmentRepository
{
    public interface INurseAssignmentRepository : IGenericRepository<NurseAssignment>
    {
        Task<IEnumerable<NurseAssignment>> GetAssignmentsByNurseIdAsync(int nurseId);
        Task<IEnumerable<NurseAssignment>> GetAssignmentsByAdmissionIdAsync(int admissionId);
        Task<IEnumerable<NurseAssignment>> GetAssignmentsByErVisitIdAsync(int erVisitId);
    }
}
