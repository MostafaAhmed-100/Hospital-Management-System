using HospitalManagementSystem.Data.Models.Pharmacys;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.PharmacyInventoryRepository
{
    public interface IPharmacyInventoryRepository : IGenericRepository<PharmacyInventory>
    {
        Task<PharmacyInventory?> CheckMedicineStockAsync(int pharmacyId, int medicineId);
    }
}
