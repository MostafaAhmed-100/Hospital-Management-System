using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models.Pharmacys;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.PharmacyInventoryRepository
{
    public class PharmacyInventoryRepository : GenericRepository<PharmacyInventory>, IPharmacyInventoryRepository
    {
        public PharmacyInventoryRepository(AppDbContext appDbcontext) : base(appDbcontext)
        {
        }

        public async Task<PharmacyInventory?> CheckMedicineStockAsync(int pharmacyId, int medicineId)
        {
            var Inventory = await _AppDbcontext.PharmacyInventories
                .Where(x => x.PharmacyId == pharmacyId && x.MedicineId == medicineId)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return Inventory;
        }
    }
}
