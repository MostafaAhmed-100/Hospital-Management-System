using HospitalManagementSystem.Data.Models.OutpatientVisits;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.PatientRepository
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        Task<Patient?> GetPatientWithMedicalHistoryAsync(int patientId);
    }
}
