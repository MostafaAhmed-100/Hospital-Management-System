using HospitalManagementSystem.Data.Models.Clinics_Doctors;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.ClinicRepository
{
    public interface IClinicRepository : IGenericRepository<Clinic>
    {
        Task<IEnumerable<Clinic?>> GetClinicsByDepartmentAsync(int departmentId);
    }
}
