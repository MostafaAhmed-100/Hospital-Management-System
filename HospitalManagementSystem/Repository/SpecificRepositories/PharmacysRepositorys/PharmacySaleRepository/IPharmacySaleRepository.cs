using HospitalManagementSystem.Data.Models.Pharmacys;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.PharmacysRepository.PharmacySaleRepository
{
    public interface IPharmacySaleRepository : IGenericRepository<PharmacySale>
    {
        Task<PharmacySale?> GetSaleWithItemsAsync(int id);
    }
}