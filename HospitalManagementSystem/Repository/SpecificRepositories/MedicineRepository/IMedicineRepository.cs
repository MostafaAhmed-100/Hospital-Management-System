using HospitalManagementSystem.Data.Models.Pharmacys;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.MedicineRepository
{
    public interface IMedicineRepository : IGenericRepository<Medicine>
    {
        Task<IEnumerable<Medicine?>> SearchMedicinesByNameAsync(string name);
    }
}
