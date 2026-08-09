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
        public async Task<IEnumerable<(string MedicineName, decimal TotalRevenue)>> GetTopRevenueGeneratingMedicinesAsync()
        {
            var topMedicines = await _AppDbcontext.SaleItems
                .Include(x => x.Medicine)
                .GroupBy(x => x.Medicine.Name)
                .Select(g => new
                {
                    MedicineName = g.Key,
                    TotalRevenue = g.Sum(s => s.Quantity * s.UnitPrice)
                })
                .OrderByDescending(m => m.TotalRevenue)
                .Take(5)
                .ToListAsync();

            return topMedicines.Select(x => (x.MedicineName, x.TotalRevenue));
        }
    }
}