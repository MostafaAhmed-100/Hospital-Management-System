using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models.Pharmacys;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.PharmacysRepository.PharmacySaleRepository
{
    public class PharmacySaleRepository : GenericRepository<PharmacySale>, IPharmacySaleRepository
    {
        public PharmacySaleRepository(AppDbContext appDbcontext) : base(appDbcontext)
        {
        }

        public async Task<PharmacySale?> GetSaleWithItemsAsync(int id)
        {
            var sale = await _AppDbcontext.PharmacySales
                .Include(x => x.SaleItems)
                .ThenInclude(i => i.Medicine)
                .AsSplitQuery()
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync(x => x.Id == id);

            return sale;
        }
    }
}