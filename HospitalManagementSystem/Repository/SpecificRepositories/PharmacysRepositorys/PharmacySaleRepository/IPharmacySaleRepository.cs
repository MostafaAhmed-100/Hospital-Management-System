using HospitalManagementSystem.Data.Models.Pharmacys;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.PharmacysRepository.PharmacySaleRepository
{
    public interface IPharmacySaleRepository : IGenericRepository<PharmacySale>
    {
        Task<PharmacySale?> GetSaleWithItemsAsync(int id);
        Task<IEnumerable<(string PharmacyName, decimal TotalRevenue)>> GetTotalRevenueByPharmacyAsync();
        Task<IEnumerable<(string Category, int Count)>> GetSalesDistributionByPrescriptionAsync();
    }
}