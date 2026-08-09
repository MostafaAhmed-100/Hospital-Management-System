using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.Pharmacys;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.PharmacysRepository.PrescriptionRepository
{
    public interface IPrescriptionRepository : IGenericRepository<Prescription>
    {
        Task<Prescription?> GetPrescriptionWithItemsAsync(int prescriptionId);
        Task<IEnumerable<(PrescriptionStatus Status, int Count)>> GetPrescriptionStatusDistributionAsync();
        Task<IEnumerable<(string DoctorName, int Count)>> GetTopPrescribingDoctorsAsync();
    }
}
