using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models.Pharmacys;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.PharmacysRepository.PharmacyRepository
{
    public class PharmacyRepository : GenericRepository<Pharmacy>, IPharmacyRepository
    {
        public PharmacyRepository(AppDbContext appDbcontext) : base(appDbcontext)
        {
        }

        public async Task<Pharmacy?> GetPharmacyWithInventoryAsync(int pharmacyId)
        {
            var Pharmacy = await _AppDbcontext.Pharmacies
                .Include(x => x.PharmacyInventories)
                .ThenInclude(i => i.Medicine)
                .AsSplitQuery()
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync(x => x.Id == pharmacyId);
            return Pharmacy;
        }
        public async Task<IEnumerable<(string PharmacyName, int SalesCount)>> GetTopPharmaciesBySalesCountAsync()
        {
            var topPharmacies = await _AppDbcontext.Pharmacies
                .Where(x => !x.IsDeleted)
                .Select(p => new
                {
                    p.Name,
                    SalesCount = p.PharmacySales.Count()
                })
                .OrderByDescending(p => p.SalesCount)
                .Take(5)
                .ToListAsync();

            return topPharmacies.Select(x => (x.Name, x.SalesCount));
        }

        public async Task<IEnumerable<(string PharmacyName, int InventoryCount)>> GetTopPharmaciesByInventorySizeAsync()
        {
            var topInventories = await _AppDbcontext.Pharmacies
                .Where(x => !x.IsDeleted)
                .Select(p => new
                {
                    p.Name,
                    InventoryCount = p.PharmacyInventories.Count()
                })
                .OrderByDescending(p => p.InventoryCount)
                .Take(5)
                .ToListAsync();

            return topInventories.Select(x => (x.Name, x.InventoryCount));
        }
    }
}