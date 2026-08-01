using HospitalManagementSystem.Data.Models.OutpatientVisits;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.MedicalRecordRepository
{
    public interface IMedicalRecordRepository : IGenericRepository<MedicalRecord>
    {
        Task<IEnumerable<MedicalRecord?>> GetRecordsByPatientIdAsync(int patientId);
    }
}