using HospitalManagementSystem.Data.Models.Clinics_Doctors;
using HospitalManagementSystem.Data.Models.Inpatient;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.InpatientRepositorys.AdmissionRepository
{
    public interface IAdmissionRepository : IGenericRepository<Admission>
    {
        Task<IEnumerable<Admission>> GetActiveAdmissionsAsync();
        Task<Admission?> GetActiveAdmissionByBedIdAsync(int bedId);
        Task<int> GetActiveAdmissionsCountAsync();
        Task<IEnumerable<Doctor?>> GetTopAdmittingDoctorsAsync();
    }
}