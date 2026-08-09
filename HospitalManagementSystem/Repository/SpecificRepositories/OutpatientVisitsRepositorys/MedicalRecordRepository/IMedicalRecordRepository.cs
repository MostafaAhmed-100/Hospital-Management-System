using HospitalManagementSystem.Data.Models.OutpatientVisits;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.OutpatientVisitsRepository.MedicalRecordRepository
{
    public interface IMedicalRecordRepository : IGenericRepository<MedicalRecord>
    {
        Task<IEnumerable<MedicalRecord?>> GetRecordsByPatientIdAsync(int patientId);
        Task<IEnumerable<(string Diagnosis, int Count)>> GetTopDiagnosesAsync();
        Task<int> GetTodayMedicalRecordsCountAsync();
    }
}