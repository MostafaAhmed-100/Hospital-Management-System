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
    }
}
