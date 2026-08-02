using HospitalManagementSystem.Data.Models.Pharmacys;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.PharmacySaleRepository
{
    public interface IPharmacySaleRepository : IGenericRepository<PharmacySale>
    {
        Task<PharmacySale?> GetSaleWithItemsAsync(int id);
    }
}