using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.Nursing_Staff;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.Nursing_StaffRepositorys.NurseRepository
{
    public interface INurseRepository : IGenericRepository<Nurse>
    {
        Task<IEnumerable<Nurse>> GetNursesByShiftAsync(ShiftType shift);
        Task<IEnumerable<Nurse>> GetNursesByWardAsync(string wardSpecialization);
        Task<IEnumerable<(ShiftType Shift, int Count)>> GetNursesDistributionByShiftAsync();
        Task<IEnumerable<(string WardSpecialization, int Count)>> GetTopWardSpecializationsAsync();
    }
}
