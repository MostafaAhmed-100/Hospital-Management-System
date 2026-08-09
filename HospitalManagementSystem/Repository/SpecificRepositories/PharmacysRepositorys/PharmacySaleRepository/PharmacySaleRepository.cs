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
        public async Task<IEnumerable<(string PharmacyName, decimal TotalRevenue)>> GetTotalRevenueByPharmacyAsync()
        {
            var revenues = await _AppDbcontext.PharmacySales
                .Include(x => x.Pharmacy)
                .GroupBy(x => x.Pharmacy.Name)
                .Select(g => new
                {
                    PharmacyName = g.Key,
                    TotalRevenue = g.Sum(s => s.TotalAmount)
                })
                .OrderByDescending(r => r.TotalRevenue)
                .ToListAsync();

            return revenues.Select(x => (x.PharmacyName, x.TotalRevenue));
        }

        public async Task<IEnumerable<(string Category, int Count)>> GetSalesDistributionByPrescriptionAsync()
        {
            var withPrescriptionCount = await _AppDbcontext.PharmacySales
                .Where(x => x.PrescriptionId != null)
                .CountAsync();

            var withoutPrescriptionCount = await _AppDbcontext.PharmacySales
                .Where(x => x.PrescriptionId == null)
                .CountAsync();

            var result = new List<(string Category, int Count)>
            {
                ("With Prescription", withPrescriptionCount),
                ("Without Prescription (OTC)", withoutPrescriptionCount)
            };

            return result;
        }
    }
}