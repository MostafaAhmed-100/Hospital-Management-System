using HospitalManagementSystem.Data.Models.Pharmacys;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.PrescriptionRepository
{
    public interface IPrescriptionRepository : IGenericRepository<Prescription>
    {
        Task<Prescription?> GetPrescriptionWithItemsAsync(int prescriptionId);
    }
}
