using HospitalManagementSystem.Data.Models.Pharmacys;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.PharmacysRepository.PharmacyRepository
{
    public interface IPharmacyRepository : IGenericRepository<Pharmacy>
    {
        Task<Pharmacy?> GetPharmacyWithInventoryAsync(int pharmacyId);
        Task<IEnumerable<(string PharmacyName, int InventoryCount)>> GetTopPharmaciesByInventorySizeAsync();
        Task<IEnumerable<(string PharmacyName, int SalesCount)>> GetTopPharmaciesBySalesCountAsync();
    }
}
