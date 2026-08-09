using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.Pharmacys;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.PharmacysRepository.PrescriptionItemRepository
{
    public interface IPrescriptionItemRepository : IGenericRepository<PrescriptionItem>
    {
        Task<IEnumerable<PrescriptionItem>> GetItemsByPrescriptionIdAsync(int prescriptionId);
    }
}