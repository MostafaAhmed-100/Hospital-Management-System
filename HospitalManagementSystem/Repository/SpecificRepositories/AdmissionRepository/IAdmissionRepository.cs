using HospitalManagementSystem.Data.Models.Inpatient;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.AdmissionRepository
{
    public interface IAdmissionRepository : IGenericRepository<Admission>
    {
        Task<IEnumerable<Admission>> GetActiveAdmissionsAsync();
        Task<Admission?> GetActiveAdmissionByBedIdAsync(int bedId);
    }
}