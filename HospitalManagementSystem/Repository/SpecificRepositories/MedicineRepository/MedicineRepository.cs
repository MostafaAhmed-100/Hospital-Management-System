using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models.Pharmacys;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.MedicineRepository
{
    public class MedicineRepository : GenericRepository<Medicine>, IMedicineRepository
    {
        public MedicineRepository(AppDbContext appDbcontext) : base(appDbcontext)
        {
        }

        public async Task<IEnumerable<Medicine?>> SearchMedicinesByNameAsync(string name)
        {
            var Medicines = await _AppDbcontext.Medicines
                .Where(x => x.Name.Contains(name))
                .AsNoTracking()
                .ToListAsync();

            return Medicines;
        }
    }
}
