using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models.Pharmacys;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.PharmacysRepository.MedicineRepository
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
        public async Task<IEnumerable<(string MedicineName, int SalesCount)>> GetTopSellingMedicinesAsync()
        {
            var topMedicines = await _AppDbcontext.Medicines
                .Where(x => !x.IsDeleted)
                .Select(m => new
                {
                    m.Name,
                    SalesCount = m.SaleItems.Count()
                })
                .OrderByDescending(m => m.SalesCount)
                .Take(5)
                .ToListAsync();

            return topMedicines.Select(x => (x.Name, x.SalesCount));
        }

        public async Task<IEnumerable<(string Category, int Count)>> GetMedicinePrescriptionDistributionAsync()
        {
            var prescriptionCount = await _AppDbcontext.Medicines
                .Where(x => !x.IsDeleted && x.RequiresPrescription)
                .CountAsync();

            var otcCount = await _AppDbcontext.Medicines
                .Where(x => !x.IsDeleted && !x.RequiresPrescription)
                .CountAsync();

            var result = new List<(string Category, int Count)>
            {
                ("Prescription Required", prescriptionCount),
                ("OTC (Over The Counter)", otcCount)
            };

            return result;
        }
    }
}