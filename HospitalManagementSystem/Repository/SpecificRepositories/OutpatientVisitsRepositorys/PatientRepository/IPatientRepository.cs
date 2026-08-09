using HospitalManagementSystem.Data.Models.OutpatientVisits;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.OutpatientVisitsRepository.PatientRepository
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        Task<Patient?> GetPatientWithMedicalHistoryAsync(int patientId);
        Task<IEnumerable<(string PatientName, int AppointmentsCount)>> GetTopFrequentPatientsAsync();
        Task<IEnumerable<(string Category, int Count)>> GetPatientInsuranceDistributionAsync();
    }
}
