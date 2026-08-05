using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.Nursing_Staff;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.Nursing_StaffRepositorys.StaffRepository
{
    public interface IStaffRepository : IGenericRepository<Staff>
    {
        Task<IEnumerable<Staff>> GetStaffByClinicIdAsync(int clinicId);
        Task<IEnumerable<Staff>> GetStaffByRoleAsync(StaffRole role);
    }
}
