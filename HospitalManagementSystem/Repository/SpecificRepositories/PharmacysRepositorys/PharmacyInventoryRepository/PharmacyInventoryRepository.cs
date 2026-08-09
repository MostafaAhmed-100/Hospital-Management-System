using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models.Pharmacys;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.PharmacysRepository.PharmacyInventoryRepository
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
        public async Task<IEnumerable<(string MedicineName, string PharmacyName, int Quantity)>> GetLowStockMedicinesAsync()
        {
            var lowStock = await _AppDbcontext.PharmacyInventories
                .Include(x => x.Medicine)
                .Include(x => x.Pharmacy)
                .OrderBy(x => x.Quantity)
                .Take(5)
                .ToListAsync();

            return lowStock.Select(x => (x.Medicine.Name, x.Pharmacy.Name, x.Quantity));
        }

        public async Task<IEnumerable<(string MedicineName, string PharmacyName, DateTime ExpiryDate, int Quantity)>> GetExpiringSoonMedicinesAsync()
        {
            var thresholdDate = DateTime.Today.AddDays(60);

            var expiringSoon = await _AppDbcontext.PharmacyInventories
                .Include(x => x.Medicine)
                .Include(x => x.Pharmacy)
                .Where(x => x.ExpiryDate <= thresholdDate && x.ExpiryDate >= DateTime.Today && x.Quantity > 0)
                .OrderBy(x => x.ExpiryDate)
                .Take(5)
                .ToListAsync();

            return expiringSoon.Select(x => (x.Medicine.Name, x.Pharmacy.Name, x.ExpiryDate, x.Quantity));
        }
    }
}
