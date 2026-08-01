using HospitalManagementSystem.Data.Models.Clinics_Doctors;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.DepartmentRepository
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        Task<Department?> GetDepartmentWithClinicsAndDoctorsAsync(int departmentId);
    }
}
