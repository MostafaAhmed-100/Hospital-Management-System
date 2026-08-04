using HospitalManagementSystem.Data.Models.Clinics_Doctors;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.DoctorRepository
{
    public interface IDoctorRepository : IGenericRepository<Doctor>
    {
        Task<IEnumerable<Doctor?>> GetDoctorsBySpecialtyAsync(int specialtyId);
        Task<Doctor?> GetDoctorWithDepartmentAsync(int doctorId);
    }
}
