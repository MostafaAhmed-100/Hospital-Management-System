using HospitalManagementSystem.Data.Models.Pharmacys;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.PharmacyRepository
{
    public interface IPharmacyRepository : IGenericRepository<Pharmacy>
    {
        Task<Pharmacy?> GetPharmacyWithInventoryAsync(int pharmacyId);
    }
}
