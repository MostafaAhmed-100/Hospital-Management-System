using HospitalManagementSystem.Data.Models.Pharmacys;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.SaleItemRepository
{
    public interface ISaleItemRepository : IGenericRepository<SaleItem>
    {
        Task<IEnumerable<SaleItem>> GetItemsBySaleIdAsync(int saleId);
    }
}