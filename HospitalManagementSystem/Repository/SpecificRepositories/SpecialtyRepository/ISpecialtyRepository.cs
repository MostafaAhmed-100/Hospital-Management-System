using HospitalManagementSystem.Data.Models.Clinics_Doctors;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.SpecialtyRepository
{
    public interface ISpecialtyRepository : IGenericRepository<Specialty>
    {
        Task<Specialty?> GetSpecialtyWithDoctorsAsync(int specialtyId);
    }
}
