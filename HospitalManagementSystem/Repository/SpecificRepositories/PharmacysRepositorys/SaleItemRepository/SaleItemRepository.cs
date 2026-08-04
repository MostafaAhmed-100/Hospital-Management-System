using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models.Pharmacys;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.PharmacysRepository.SaleItemRepository
{
    public class SaleItemRepository : GenericRepository<SaleItem>, ISaleItemRepository
    {
        public SaleItemRepository(AppDbContext appDbcontext) : base(appDbcontext)
        {
        }

        public async Task<IEnumerable<SaleItem>> GetItemsBySaleIdAsync(int saleId)
        {
            var items = await _AppDbcontext.SaleItems
                .Include(x => x.Medicine)
                .Where(x => x.SaleId == saleId)
                .AsNoTracking()
                .ToListAsync();

            return items;
        }
    }
}